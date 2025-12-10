using System.Collections.Generic;
using System.Linq;

namespace ExCSS;

internal static class ValueConverterExtensions
{
    extension(IValueConverter converter)
    {
        public IPropertyValue ConvertDefault()
        {
            return converter.Convert([]);
        }

        public IPropertyValue VaryStart(List<Token> list)
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

        public IPropertyValue VaryAll(List<Token> list)
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

        public IValueConverter Many(int min = 1, int max = ushort.MaxValue)
        {
            return new OneOrMoreValueConverter(converter, min, max);
        }

        public IValueConverter FromList()
        {
            return new ListValueConverter(converter);
        }
    }

    public static IValueConverter ToConverter<T>(this Dictionary<string, T> values)
    {
        return new DictionaryValueConverter<T>(values);
    }

    extension(IValueConverter converter)
    {
        public IValueConverter Periodic(params string[] labels)
        {
            return new PeriodicValueConverter(converter, labels);
        }

        public IValueConverter RequiresEnd(IValueConverter endConverter)
        {
            return new EndListValueConverter(converter, endConverter);
        }

        public IValueConverter Required()
        {
            return new RequiredValueConverter(converter);
        }

        public IValueConverter Option()
        {
            return new OptionValueConverter(converter);
        }

        public IValueConverter For(params string[] labels)
        {
            return new ConstraintValueConverter(converter, labels);
        }

        public IValueConverter Option<T>(T defaultValue)
        {
            return new OptionValueConverter<T>(converter);
        }

        public IValueConverter Or(IValueConverter secondary)
        {
            return new OrValueConverter(converter, secondary);
        }

        public IValueConverter Or(string keyword)
        {
            return converter.Or<object>(keyword, null);
        }

        public IValueConverter Or<T>(string keyword, T value)
        {
            var identifier = new IdentifierValueConverter<T>(keyword, value);
            return new OrValueConverter(converter, identifier);
        }

        public IValueConverter OrNone()
        {
            return converter.Or(Keywords.None);
        }

        public IValueConverter OrDefault()
        {
            return converter.OrInherit().Or(Keywords.Initial);
        }

        public IValueConverter OrDefault<T>(T value)
        {
            return converter.OrInherit().Or(Keywords.Initial, value);
        }

        public IValueConverter OrInherit()
        {
            return converter.Or(Keywords.Inherit);
        }

        public IValueConverter OrAuto()
        {
            return converter.Or(Keywords.Auto);
        }

        public IValueConverter OrGlobalValue()
        {
            return converter.OrInherit()
                .Or(Keywords.Initial)
                .Or(Keywords.Revert)
                .Or(Keywords.RevertLayer)
                .Or(Keywords.Unset);
        }

        public IValueConverter ConditionalStartsWithKeyword(string when, params string[] keywords)
        {
            return new ConditionalStartsWithValueConverter(when, converter, keywords);
        }

        public IValueConverter StartsWithKeyword(string keyword)
        {
            return new StartsWithValueConverter(TokenType.Ident, keyword, converter);
        }

        public IValueConverter StartsWithDelimiter()
        {
            return new StartsWithValueConverter(TokenType.Delim, "/", converter);
        }

        public IValueConverter WithCurrentColor()
        {
            return converter.Or(Keywords.CurrentColor, Color.Transparent);
        }

        public IValueConverter WithFallback(int defaultValue)
        {
            return new FallbackValueConverter(converter, TokenValue.FromNumber(defaultValue));
        }
    }
}