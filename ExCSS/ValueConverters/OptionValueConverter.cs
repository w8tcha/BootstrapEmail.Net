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

using ExCSS.Model;
using ExCSS.StyleProperties;
using ExCSS.Tokens;

namespace ExCSS.ValueConverters;

internal sealed class OptionValueConverter : IValueConverter
{
    private readonly IValueConverter _converter;

    public OptionValueConverter(IValueConverter converter)
    {
        _converter = converter;
    }

    public IPropertyValue Convert(IEnumerable<Token> value)
    {
        return value.Any() ? _converter.Convert(value) : new OptionValue(value);
    }

    public IPropertyValue Construct(Property[] properties)
    {
        return _converter.Construct(properties) ?? new OptionValue([]);
    }

    private sealed class OptionValue : IPropertyValue
    {
        public OptionValue(IEnumerable<Token> tokens)
        {
            Original = new TokenValue(tokens);
        }

        public string CssText => string.Empty;

        public TokenValue Original { get; }

        public TokenValue ExtractFor(string name)
        {
            return null;
        }
    }
}

internal sealed class OptionValueConverter<T> : IValueConverter
{
    private readonly IValueConverter _converter;

    public OptionValueConverter(IValueConverter converter)
    {
        _converter = converter;
    }

    public IPropertyValue Convert(IEnumerable<Token> value)
    {
        return value.Any() ? _converter.Convert(value) : new OptionValue(value);
    }

    public IPropertyValue Construct(Property[] properties)
    {
        return _converter.Construct(properties) ?? new OptionValue([]);
    }

    private sealed class OptionValue : IPropertyValue
    {
        public OptionValue(IEnumerable<Token> tokens)
        {
            Original = new TokenValue(tokens);
        }

        public string CssText => string.Empty;

        public TokenValue Original { get; }

        public TokenValue ExtractFor(string name)
        {
            return null;
        }
    }
}