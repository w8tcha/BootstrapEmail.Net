namespace ExCSS;

internal static class CharExtensions
{
    extension(char c)
    {
        public int FromHex()
        {
            return c.IsDigit() ? c - Symbols.Zero : c - (c.IsLowercaseAscii() ? Symbols.CapitalW : Symbols.Seven);
        }

        public string ToHex()
        {
            return ((int) c).ToString("x");
        }

        public bool IsInRange(int lower, int upper)
        {
            return c >= lower && c <= upper;
        }

        public bool IsNormalQueryCharacter()
        {
            return c.IsInRange(Symbols.ExclamationMark, Symbols.Tilde) &&
                   c != Symbols.DoubleQuote &&
                   c != Symbols.CurvedQuote && c != Symbols.Num &&
                   c != Symbols.LessThan && c != Symbols.GreaterThan;
        }

        public bool IsNormalPathCharacter()
        {
            return c.IsInRange(Symbols.Space, Symbols.Tilde) &&
                   c != Symbols.DoubleQuote &&
                   c != Symbols.CurvedQuote && c != Symbols.Num &&
                   c != Symbols.LessThan && c != Symbols.GreaterThan &&
                   c != Symbols.Space && c != Symbols.QuestionMark;
        }

        public bool IsUppercaseAscii()
        {
            return c is >= Symbols.CapitalA and <= Symbols.CapitalZ;
        }

        public bool IsLowercaseAscii()
        {
            return c is >= Symbols.LowerA and <= Symbols.LowerZ;
        }

        public bool IsAlphanumericAscii()
        {
            return c.IsDigit() || c.IsUppercaseAscii() || c.IsLowercaseAscii();
        }

        public bool IsHex()
        {
            return c.IsDigit() || c is >= Symbols.CapitalA and <= Symbols.CapitalF ||
                   c is >= Symbols.LowerA and <= Symbols.LowerF;
        }

        public bool IsNonAscii()
        {
            return c != Symbols.EndOfFile && c >= Symbols.ExtendedAsciiStart;
        }

        public bool IsNonPrintable()
        {
            return c is /*>= Symbols.Null and*/ <= Symbols.Backspace 
                or >= Symbols.ShiftOut and <= Symbols.UnitSeparator
                or >= Symbols.Delete and < Symbols.NonBreakingSpace;
        }

        public bool IsLetter()
        {
            return IsUppercaseAscii(c) || IsLowercaseAscii(c);
        }

        public bool IsName()
        {
            return c.IsNonAscii() || c.IsLetter() || c == Symbols.Underscore || c == Symbols.Minus || c.IsDigit();
        }

        public bool IsNameStart()
        {
            return c.IsNonAscii() || c.IsUppercaseAscii() || c.IsLowercaseAscii() || c == Symbols.Underscore;
        }

        public bool IsLineBreak()
        {
            return c is Symbols.LineFeed or Symbols.CarriageReturn;
        }

        public bool IsSpaceCharacter()
        {
            return c is Symbols.Space or Symbols.Tab or Symbols.LineFeed or Symbols.CarriageReturn or Symbols.FormFeed;
        }

        public bool IsDigit()
        {
            return c is >= Symbols.Zero and <= Symbols.Nine;
        }
    }

    // HTML forbids the use of Universal Character Set / Unicode code points
    // - 0 to 31, except 9, 10, and 13 C0 control characters
    // - 127 DEL character
    // - 128 to 159 (0x80 to 0x9F, C1 control characters
    // - 55296 to 57343 (0xD800 – xDFFF, UTF-16 surrogate halves)
    // - 65534 and 65535 (xFFFE – xFFFF, non-characters, related to xFEFF, the byte order mark)
    public static bool IsInvalid(this int c)
    {
        return c == 0 || c > Symbols.MaximumCodepoint ||
               c is > Symbols.UTF16SurrogateMin and < Symbols.UTF16SurrogateMax;
    }

    extension(char c)
    {
        public bool IsOneOf(char a, char b)
        {
            return a == c || b == c;
        }

        public bool IsOneOf(char o1, char o2, char o3)
        {
            return c == o1 || c == o2 || c == o3;
        }

        public bool IsOneOf(char o1, char o2, char o3, char o4)
        {
            return c == o1 || c == o2 || c == o3 || c == o4;
        }
    }
}