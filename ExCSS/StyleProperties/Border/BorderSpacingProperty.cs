using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;
using ExCSS.Values;

namespace ExCSS.StyleProperties.Border
{
    internal sealed class BorderSpacingProperty : Property
    {
        private static readonly IValueConverter StyleConverter =
            Converters.LengthConverter.Many(1, 2).OrDefault(Length.Zero);

        internal BorderSpacingProperty()
            : base(PropertyNames.BorderSpacing, PropertyFlags.Inherited)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}