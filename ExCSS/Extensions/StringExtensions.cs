using System;
using System.Globalization;
using System.Linq;

namespace ExCSS;

internal static class StringExtensions
{
    public static bool Has(this string value, char chr, int index = 0)
    {
        return value != null && value.Length > index && value[index] == chr;
    }

    public static bool Contains(this string[] list, string element,
        StringComparison comparison = StringComparison.Ordinal)
    {
        return list.Any(t => t.Equals(element, comparison));
    }

    public static bool Is(this string current, string other)
    {
        return string.Equals(current, other, StringComparison.Ordinal);
    }

    public static bool Isi(this string current, string other)
    {
        return string.Equals(current, other, StringComparison.OrdinalIgnoreCase);
    }

    public static bool IsOneOf(this string current, string item1, string item2)
    {
        return current.Is(item1) || current.Is(item2);
    }

    public static string StylesheetString(this string current)
    {
        var builder = Pool.NewStringBuilder();
        builder.Append(Symbols.DoubleQuote);

        if (!string.IsNullOrEmpty(current))
            for (var i = 0; i < current.Length; i++)
            {
                var character = current[i];

                switch (character)
                {
                    case Symbols.Null:
                        throw new ParseException("Unable to parse null symbol");
                    case Symbols.DoubleQuote:
                    case Symbols.ReverseSolidus:
                        builder.Append(Symbols.ReverseSolidus).Append(character);
                        break;
                    default:
                        if (character.IsInRange(Symbols.StartOfHeading, Symbols.UnitSeparator)
                            || character == Symbols.CurlyBracketOpen)
                        {
                            builder.Append(Symbols.ReverseSolidus)
                                .Append(character.ToHex())
                                .Append(i + 1 != current.Length ? " " : "");
                        }
                        else
                        {
                            builder.Append(character);
                        }

                        break;
                }
            }

        builder.Append(Symbols.DoubleQuote);
        return builder.ToPool();
    }

    public static string StylesheetFunction(this string current, string argument)
    {
        return string.Concat(current, "(", argument, ")");
    }

    public static string StylesheetUrl(this string current)
    {
        var argument = current.StylesheetString();
        return FunctionNames.Url.StylesheetFunction(argument);
    }

    public static string StylesheetUnit(this string current, out float result)
    {
        if (!string.IsNullOrEmpty(current))
        {
            var firstLetter = current.Length;

            while (!current[firstLetter - 1].IsDigit() && --firstLetter > 0)
            {
                // Intentional empty.
            }

            var parsed = float.TryParse(current.Substring(0, firstLetter), NumberStyles.Any,
                CultureInfo.InvariantCulture, out result);

            if (firstLetter > 0 && parsed) return current.Substring(firstLetter);
        }

        result = default;
        return null;
    }
}