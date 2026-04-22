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

using System;
using System.Collections.Generic;
using System.Linq;

using ExCSS.Extensions;
using ExCSS.Factories;
using ExCSS.Model;
using ExCSS.StyleProperties;
using ExCSS.Tokens;

namespace ExCSS.ValueConverters;

internal sealed class EndListValueConverter : IValueConverter
{
    private readonly IValueConverter _endConverter;
    private readonly IValueConverter _listConverter;

    public EndListValueConverter(IValueConverter listConverter, IValueConverter endConverter)
    {
        _listConverter = listConverter;
        _endConverter = endConverter;
    }

    public IPropertyValue Convert(IEnumerable<Token> value)
    {
        var items = value.ToList();
        var n = items.Count - 1;
        var values = new IPropertyValue[n + 1];

        for (var i = 0; i < n; i++)
        {
            values[i] = _listConverter.Convert(items[i]);

            if (values[i] == null) return null;
        }

        values[n] = _endConverter.Convert(items[n]);
        return values[n] != null ? new ListValue(values, value) : null;
    }

    public IPropertyValue Construct(Property[] properties)
    {
        var valueList = new List<List<Token>>[properties.Length];
        var dummies = new Property[properties.Length];
        var max = 0;

        for (var i = 0; i < properties.Length; i++)
        {
            var value = properties[i].DeclaredValue;
            valueList[i] = value != null ? value.Original.ToList() : [];

            dummies[i] = PropertyFactory.Instance.CreateLonghand(properties[i].Name);
            max = Math.Max(max, valueList[i].Count);
        }

        var values = new IPropertyValue[max];

        for (var i = 0; i < max; i++)
        {
            for (var j = 0; j < dummies.Length; j++)
            {
                var list = valueList[j];
                var tokens = list.Count > i ? list[i] : Enumerable.Empty<Token>();
                dummies[j].TrySetValue(new TokenValue(tokens));
            }

            var converter = i < max - 1 ? _listConverter : _endConverter;
            values[i] = converter.Construct(dummies);
        }

        return new ListValue(values, []);
    }

    private sealed class ListValue : IPropertyValue
    {
        private readonly IPropertyValue[] _values;

        public ListValue(IPropertyValue[] values, IEnumerable<Token> tokens)
        {
            _values = values;
            Original = new TokenValue(tokens);
        }

        public string CssText
        {
            get { return string.Join(", ", _values.Select(m => m.CssText)); }
        }

        public TokenValue Original { get; }

        public TokenValue ExtractFor(string name)
        {
            var tokens = new List<Token>();

            foreach (var value in _values)
            {
                var extracted = value.ExtractFor(name);

                if (extracted == null) continue;

                if (tokens.Count > 0) tokens.Add(Token.Whitespace);

                tokens.AddRange(extracted);
            }

            return new TokenValue(tokens);
        }
    }
}