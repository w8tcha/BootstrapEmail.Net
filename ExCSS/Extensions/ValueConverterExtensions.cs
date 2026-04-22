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

using ExCSS.Enumerations;
using ExCSS.Model;
using ExCSS.StyleProperties;
using ExCSS.Tokens;
using ExCSS.ValueConverters;
using ExCSS.Values;

namespace ExCSS.Extensions;

internal static class ValueConverterExtensions
{
    public static IPropertyValue ConvertDefault(this IValueConverter converter)
    {
        return converter.Convert([]);
    }

    public static IPropertyValue VaryStart(this IValueConverter converter, List<Token> list)
    {
        for (var count = list.Count; count > 0; count--)
        {
            if (list[count - 1].Type == TokenType.Whitespace)
            {
                continue;
            }

            var value = converter.Convert(list.Take(count));

            if (value == null)
            {
                continue;
            }

            list.RemoveRange(0, count);
            list.Trim();
            return value;
        }

        return converter.ConvertDefault();
    }

    public static IPropertyValue VaryAll(this IValueConverter converter, List<Token> list)
    {
        for (var i = 0; i < list.Count; i++)
        {
            if (list[i].Type == TokenType.Whitespace)
            {
                continue;
            }

            for (var j = list.Count; j > i; j--)
            {
                var count = j - i;

                if (list[j - 1].Type == TokenType.Whitespace)
                {
                    continue;
                }

                var value = converter.Convert(list.Skip(i).Take(count));

                if (value == null) continue;
                list.RemoveRange(i, count);
                list.Trim();
                return value;
            }
        }

        return converter.ConvertDefault();
    }

    public static IValueConverter Many(this IValueConverter converter, int min = 1, int max = ushort.MaxValue)
    {
        return new OneOrMoreValueConverter(converter, min, max);
    }

    public static IValueConverter FromList(this IValueConverter converter)
    {
        return new ListValueConverter(converter);
    }

    public static IValueConverter ToConverter<T>(this Dictionary<string, T> values)
    {
        return new DictionaryValueConverter<T>(values);
    }

    public static IValueConverter Periodic(this IValueConverter converter, params string[] labels)
    {
        return new PeriodicValueConverter(converter, labels);
    }

    public static IValueConverter RequiresEnd(this IValueConverter converter, IValueConverter endConverter)
    {
        return new EndListValueConverter(converter, endConverter);
    }

    public static IValueConverter Required(this IValueConverter converter)
    {
        return new RequiredValueConverter(converter);
    }

    public static IValueConverter Option(this IValueConverter converter)
    {
        return new OptionValueConverter(converter);
    }

    public static IValueConverter For(this IValueConverter converter, params string[] labels)
    {
        return new ConstraintValueConverter(converter, labels);
    }

    public static IValueConverter Option<T>(this IValueConverter converter, T defaultValue)
    {
        return new OptionValueConverter<T>(converter);
    }

    public static IValueConverter Or(this IValueConverter converter, IValueConverter secondary)
    {
        return new OrValueConverter(converter, secondary);
    }

    public static IValueConverter Or(this IValueConverter converter, string keyword)
    {
        return converter.Or<object>(keyword, null);
    }

    public static IValueConverter Or<T>(this IValueConverter converter, string keyword, T value)
    {
        var identifier = new IdentifierValueConverter<T>(keyword, value);
        return new OrValueConverter(converter, identifier);
    }

    public static IValueConverter OrNone(this IValueConverter converter)
    {
        return converter.Or(Keywords.None);
    }

    public static IValueConverter OrDefault(this IValueConverter converter)
    {
        return converter.OrInherit().Or(Keywords.Initial);
    }

    public static IValueConverter OrDefault<T>(this IValueConverter converter, T value)
    {
        return converter.OrInherit().Or(Keywords.Initial, value);
    }

    public static IValueConverter OrInherit(this IValueConverter converter)
    {
        return converter.Or(Keywords.Inherit);
    }

    public static IValueConverter OrAuto(this IValueConverter converter)
    {
        return converter.Or(Keywords.Auto);
    }

    public static IValueConverter OrGlobalValue(this IValueConverter converter)
    {
        return converter.OrInherit()
            .Or(Keywords.Initial)
            .Or(Keywords.Revert)
            .Or(Keywords.RevertLayer)
            .Or(Keywords.Unset);
    }

    public static IValueConverter ConditionalStartsWithKeyword(this IValueConverter converter, string when, params string[] keywords)
    {
        return new ConditionalStartsWithValueConverter(when, converter, keywords);
    }

    public static IValueConverter StartsWithKeyword(this IValueConverter converter, string keyword)
    {
        return new StartsWithValueConverter(TokenType.Ident, keyword, converter);
    }

    public static IValueConverter StartsWithDelimiter(this IValueConverter converter)
    {
        return new StartsWithValueConverter(TokenType.Delim, "/", converter);
    }

    public static IValueConverter WithCurrentColor(this IValueConverter converter)
    {
        return converter.Or(Keywords.CurrentColor, Color.Transparent);
    }

    public static IValueConverter WithFallback(this IValueConverter converter, int defaultValue)
    {
        return new FallbackValueConverter(converter, TokenValue.FromNumber(defaultValue));
    }
}