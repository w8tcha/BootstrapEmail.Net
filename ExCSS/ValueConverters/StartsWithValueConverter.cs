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

using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;
using ExCSS.StyleProperties;
using ExCSS.Tokens;

namespace ExCSS.ValueConverters;

internal sealed class StartsWithValueConverter : IValueConverter
{
    private readonly IValueConverter _converter;
    private readonly string _data;
    private readonly TokenType _type;

    public StartsWithValueConverter(TokenType type, string data, IValueConverter converter)
    {
        _type = type;
        _data = data;
        _converter = converter;
    }

    public IPropertyValue Convert(IEnumerable<Token> value)
    {
        var rest = Transform(value);
        return rest != null ? CreateFrom(_converter.Convert(rest), value) : null;
    }

    public IPropertyValue Construct(Property[] properties)
    {
        var value = _converter.Construct(properties);
        return value != null ? CreateFrom(value, []) : null;
    }

    private IPropertyValue CreateFrom(IPropertyValue value, IEnumerable<Token> tokens)
    {
        return value != null ? new StartValue(_data, value, tokens) : null;
    }

    private List<Token> Transform(IEnumerable<Token> values)
    {
        using var enumerator = values.GetEnumerator();

        while (enumerator.MoveNext() && enumerator.Current.Type == TokenType.Whitespace)
        {
            //Empty on purpose.
        }

        if (enumerator.Current.Type != _type || !enumerator.Current.Data.Isi(_data)) return null;
        var list = new List<Token>();

        while (enumerator.MoveNext())
            if (enumerator.Current.Type != TokenType.Whitespace || list.Count != 0)
                list.Add(enumerator.Current);

        return list;
    }

    private sealed class StartValue : IPropertyValue
    {
        private readonly string _start;
        private readonly IPropertyValue _value;

        public StartValue(string start, IPropertyValue value, IEnumerable<Token> tokens)
        {
            _start = start;
            _value = value;
            Original = new TokenValue(tokens);
        }

        public string CssText => string.Concat(_start, " ", _value.CssText);

        public TokenValue Original { get; }

        public TokenValue ExtractFor(string name)
        {
            return _value.ExtractFor(name);
        }
    }
}