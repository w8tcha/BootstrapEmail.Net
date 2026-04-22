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
using System.Diagnostics;
using System.Linq;

using ExCSS.Extensions;
using ExCSS.Model;
using ExCSS.StyleProperties;
using ExCSS.Tokens;

namespace ExCSS.ValueConverters;

internal sealed class PeriodicValueConverter : IValueConverter
{
    private readonly IValueConverter _converter;
    private readonly string[] _labels;

    public PeriodicValueConverter(IValueConverter converter, string[] labels)
    {
        Debug.Assert(labels.Length is 0 or 4 or 2);

        _converter = converter;
        _labels = labels;
    }

    public IPropertyValue Convert(IEnumerable<Token> value)
    {
        var list = new List<Token>(value);
        var options = new IPropertyValue[_labels.Length == 0 ? 4 : _labels.Length];

        if (list.Count == 0) return null;

        for (var i = 0; i < options.Length && list.Count != 0; i++)
        {
            options[i] = _converter.VaryStart(list);

            if (options[i] == null) return null;
        }

        return list.Count == 0 ? new PeriodicValue(options, value, _labels) : null;
    }

    public IPropertyValue Construct(Property[] properties)
    {
        var options = new IPropertyValue[_labels.Length];

        for(var i = 0; i < _labels.Length; i++)
            options[i] = _converter.Construct([.. properties.Where(m => m.Name == _labels[i])]);

        return options.All(opt => opt != null)
            ? new PeriodicValue(options, [], _labels)
            : null;
    }

    private sealed class PeriodicValue : IPropertyValue
    {
        private readonly string[] _labels;
        private readonly IPropertyValue[] _values;

        public PeriodicValue(IPropertyValue[] options, IEnumerable<Token> tokens, string[] labels)
        {
            Debug.Assert(labels.Length is 0 or 2 or 4);

            _values = new IPropertyValue[labels.Length == 0 ? 4 : labels.Length];

            if (_values.Length is 0 or 4)
            {
                //Top, right, bottom, left
                _values[0] = options[0];
                _values[1] = options[1] ?? _values[0];
                _values[2] = options[2] ?? _values[0];
                _values[3] = options[3] ?? _values[1];
            }
            else
            {
                _values[0] = options[0];
                _values[1] = options[1] ?? _values[0];
            }

            Original = new TokenValue(tokens);
            _labels = labels;
        }

        private string[] Values
        {
            get
            {
                if (_values.Length is 0 or 4)
                {
                    var top = _values[0].CssText;
                    var right = _values[1].CssText;
                    var bottom = _values[2].CssText;
                    var left = _values[3].CssText;

                    if (!right.Is(left))
                        return [top, right, bottom, left];

                    if (!top.Is(bottom))
                        return [top, right, bottom];

                    return right.Is(top)
                        ? [top]
                        : [top, right];
                }

                var first = _values[0].CssText;
                var second = _values[1].CssText;

                return first.Is(second)
                    ? [first]
                    : [first, second];
            }
        }

        public string CssText => string.Join(" ", Values);

        public TokenValue Original { get; }

        public TokenValue ExtractFor(string name)
        {
            if (name.Is(_labels[0])) return _values[0].Original;

            if (name.Is(_labels[1])) return _values[1].Original;

            if (_labels.Length == 4)
            {
                if (name.Is(_labels[2]))
                    return _values[2].Original;

                if (name.Is(_labels[3]))
                    return _values[3].Original;
            }

            return null;
        }
    }
}