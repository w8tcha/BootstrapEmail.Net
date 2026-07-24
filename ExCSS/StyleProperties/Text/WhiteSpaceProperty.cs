using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Text
{
    internal sealed class WhiteSpaceProperty : Property
    {
        private static readonly IValueConverter StyleConverter =
            Converters.WhitespaceConverter.OrDefault(Whitespace.Normal);

        internal WhiteSpaceProperty()
            : base(PropertyNames.WhiteSpace, PropertyFlags.Inherited)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}