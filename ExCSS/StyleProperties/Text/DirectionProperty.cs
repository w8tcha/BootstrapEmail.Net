using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Text
{
    internal sealed class DirectionProperty : Property
    {
        private static readonly IValueConverter StyleConverter =
            Converters.DirectionModeConverter.OrDefault(DirectionMode.Ltr);

        internal DirectionProperty()
            : base(PropertyNames.Direction, PropertyFlags.Inherited)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}