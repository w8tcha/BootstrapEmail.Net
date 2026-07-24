using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Border
{
    internal sealed class BorderStyleProperty : ShorthandProperty
    {
        private static readonly IValueConverter StyleConverter = Converters.LineStyleConverter.Periodic(
            PropertyNames.BorderTopStyle, PropertyNames.BorderRightStyle, PropertyNames.BorderBottomStyle,
            PropertyNames.BorderLeftStyle).OrDefault();

        internal BorderStyleProperty()
            : base(PropertyNames.BorderStyle)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}