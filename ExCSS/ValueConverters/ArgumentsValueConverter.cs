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

internal sealed class ArgumentsValueConverter : IValueConverter
{
    private readonly IValueConverter[] _converters;

    public ArgumentsValueConverter(params IValueConverter[] converters)
    {
        _converters = converters;
    }

    public IPropertyValue Convert(IEnumerable<Token> value)
    {
        var items = value.ToList();
        var length = _converters.Length;

        if (items.Count > length) return null;

        var args = new IPropertyValue[length];

        for (var i = 0; i < length; i++)
        {
            var item = i < items.Count ? items[i] : Enumerable.Empty<Token>();
            args[i] = _converters[i].Convert(item);

            if (args[i] == null) return null;
        }

        return new ArgumentsValue(args, value);
    }

    public IPropertyValue Construct(Property[] properties)
    {
        return properties.Guard<ArgumentsValue>();
    }

    private sealed class ArgumentsValue : IPropertyValue
    {
        private readonly IPropertyValue[] _arguments;

        public ArgumentsValue(IPropertyValue[] arguments, IEnumerable<Token> tokens)
        {
            _arguments = arguments;
            Original = new TokenValue(tokens);
        }

        public string CssText
        {
            get
            {
                var texts = _arguments.Where(m => !string.IsNullOrEmpty(m.CssText)).Select(m => m.CssText);
                return string.Join(", ", texts);
            }
        }

        public TokenValue Original { get; }

        public TokenValue ExtractFor(string name)
        {
            return Original;
        }
    }
}