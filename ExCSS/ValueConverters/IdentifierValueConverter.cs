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

using ExCSS.Extensions;
using ExCSS.Model;
using ExCSS.StyleProperties;
using ExCSS.Tokens;

namespace ExCSS.ValueConverters;

internal sealed class IdentifierValueConverter : IValueConverter
{
    private readonly Func<IEnumerable<Token>, string> _converter;

    public IdentifierValueConverter(Func<IEnumerable<Token>, string> converter)
    {
        _converter = converter;
    }

    public IPropertyValue Convert(IEnumerable<Token> value)
    {
        var result = _converter(value);
        return result != null ? new IdentifierValue(result, value) : null;
    }

    public IPropertyValue Construct(Property[] properties)
    {
        return properties.Guard<IdentifierValue>();
    }

    private sealed class IdentifierValue : IPropertyValue
    {
        public IdentifierValue(string identifier, IEnumerable<Token> tokens)
        {
            CssText = identifier;
            Original = new TokenValue(tokens);
        }

        public string CssText { get; }

        public TokenValue Original { get; }

        public TokenValue ExtractFor(string name)
        {
            return Original;
        }
    }
}

internal sealed class IdentifierValueConverter<T> : IValueConverter
{
    private readonly string _identifier;
    private readonly T _result;

    public IdentifierValueConverter(string identifier, T result)
    {
        _identifier = identifier;
        _result = result;
    }

    public IPropertyValue Convert(IEnumerable<Token> value)
    {
        return value.Is(_identifier) ? new IdentifierValue(_identifier, value) : null;
    }

    public IPropertyValue Construct(Property[] properties)
    {
        return properties.Guard<IdentifierValue>();
    }

    private sealed class IdentifierValue : IPropertyValue
    {
        public IdentifierValue(string identifier, IEnumerable<Token> tokens)
        {
            CssText = identifier;
            Original = new TokenValue(tokens);
        }

        public string CssText { get; }

        public TokenValue Original { get; }

        public TokenValue ExtractFor(string name)
        {
            return Original;
        }
    }
}