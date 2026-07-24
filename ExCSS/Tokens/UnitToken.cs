using System.Globalization;

using ExCSS.Enumerations;
using ExCSS.Model;

namespace ExCSS.Tokens
{
    internal sealed class UnitToken : Token
    {
        public UnitToken(TokenType type, string value, string dimension, TextPosition position)
            : base(type, value, position)
        {
            Unit = dimension;
        }

        public override string ToValue()
        {
            return Data + Unit;
        }

        public float Value => float.Parse(Data, CultureInfo.InvariantCulture);

        public string Unit { get; }
    }
}