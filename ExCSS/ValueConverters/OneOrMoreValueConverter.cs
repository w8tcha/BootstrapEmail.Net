// The MIT License (MIT)
//
// Copyright (c) 2024 Tyler Brinks
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Collections.Generic;
using System.Linq;

using ExCSS.Extensions;
using ExCSS.Model;
using ExCSS.StyleProperties;
using ExCSS.Tokens;

namespace ExCSS.ValueConverters;

internal sealed class OneOrMoreValueConverter : IValueConverter
{
    private readonly IValueConverter _converter;
    private readonly int _maximum;
    private readonly int _minimum;

    public OneOrMoreValueConverter(IValueConverter converter, int minimum, int maximum)
    {
        _converter = converter;
        _minimum = minimum;
        _maximum = maximum;
    }

    public IPropertyValue Convert(IEnumerable<Token> value)
    {
        var items = value.ToItems();
        var n = items.Count;

        if (n < _minimum || n > _maximum) return null;
        var values = new IPropertyValue[items.Count];

        for (var i = 0; i < n; i++)
        {
            values[i] = _converter.Convert(items[i]);

            if (values[i] == null) return null;
        }

        return new MultipleValue(values, value);
    }

    public IPropertyValue Construct(Property[] properties)
    {
        var result = properties.Guard<MultipleValue>();

        if (result == null)
        {
            var values = new IPropertyValue[properties.Length];

            for (var i = 0; i < properties.Length; i++)
            {
                var value = _converter.Construct([properties[i]]);

                if (value == null) return null;

                values[i] = value;
            }

            result = new MultipleValue(values, []);
        }

        return result;
    }

    private sealed class MultipleValue : IPropertyValue
    {
        private readonly IPropertyValue[] _values;

        public MultipleValue(IPropertyValue[] values, IEnumerable<Token> tokens)
        {
            _values = values;
            Original = new TokenValue(tokens);
        }

        public string CssText
        {
            get
            {
                return string.Join(" ",
                    _values.Where(m => !string.IsNullOrEmpty(m.CssText)).Select(m => m.CssText));
            }
        }

        public TokenValue Original { get; }

        public TokenValue ExtractFor(string name)
        {
            var tokens = new List<Token>();

            foreach (var value in _values)
            {
                var extracted = value.ExtractFor(name);

                if (extracted != null)
                {
                    if (tokens.Count > 0) tokens.Add(Token.Whitespace);

                    tokens.AddRange(extracted);
                }
            }

            return new TokenValue(tokens);
        }
    }
}