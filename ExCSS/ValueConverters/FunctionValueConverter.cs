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

internal sealed class FunctionValueConverter : IValueConverter
{
    private readonly IValueConverter _arguments;
    private readonly string _name;

    public FunctionValueConverter(string name, IValueConverter arguments)
    {
        _name = name;
        _arguments = arguments;
    }

    public IPropertyValue Convert(IEnumerable<Token> value)
    {
        var function = value.OnlyOrDefault() as FunctionToken;

        if (!Check(function)) return null;

        var args = _arguments.Convert(function.ArgumentTokens);
        return args != null ? new FunctionValue(_name, args, value) : null;
    }

    public IPropertyValue Construct(Property[] properties)
    {
        return properties.Guard<FunctionValue>();
    }

    private bool Check(FunctionToken function)
    {
        return function != null && function.Data.Equals(_name, StringComparison.OrdinalIgnoreCase);
    }

    private sealed class FunctionValue : IPropertyValue
    {
        private readonly IPropertyValue _arguments;
        private readonly string _name;

        public FunctionValue(string name, IPropertyValue arguments, IEnumerable<Token> tokens)
        {
            _name = name;
            _arguments = arguments;
            Original = new TokenValue(tokens);
        }

        public string CssText => _name.StylesheetFunction(_arguments.CssText);

        public TokenValue Original { get; }

        public TokenValue ExtractFor(string name)
        {
            return Original;
        }
    }
}