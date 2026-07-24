using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Border
{
    internal sealed class BorderTopStyleProperty : Property
    {
        private static readonly IValueConverter
            StyleConverter = Converters.LineStyleConverter.OrDefault(LineStyle.None);

        internal BorderTopStyleProperty()
            : base(PropertyNames.BorderTopStyle)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}