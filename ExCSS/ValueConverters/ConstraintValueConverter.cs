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

internal sealed class ConstraintValueConverter : IValueConverter
{
    private readonly IValueConverter _converter;
    private readonly string[] _labels;

    public ConstraintValueConverter(IValueConverter converter, string[] labels)
    {
        _converter = converter;
        _labels = labels;
    }

    public IPropertyValue Convert(IEnumerable<Token> value)
    {
        var result = _converter.Convert(value);
        return result != null ? new TransformationValueConverter(result, _labels) : null;
    }

    public IPropertyValue Construct(Property[] properties)
    {
        var filtered = properties.Where(m => _labels.Contains(m.Name));
        var existing = default(string);

        foreach (var filter in filtered)
        {
            var value = filter.Value;

            if (existing != null && value != existing) return null;

            existing = value;
        }

        var result = _converter.Construct([.. filtered.Take(1)]);
        return result != null ? new TransformationValueConverter(result, _labels) : null;
    }

    private sealed class TransformationValueConverter : IPropertyValue
    {
        private readonly string[] _labels;
        private readonly IPropertyValue _value;

        public TransformationValueConverter(IPropertyValue value, string[] labels)
        {
            _value = value;
            _labels = labels;
        }

        public string CssText => _value.CssText;

        public TokenValue Original => _value.Original;

        public TokenValue ExtractFor(string name)
        {
            return _labels.Contains(name) ? _value.ExtractFor(name) : null;
        }
    }
}