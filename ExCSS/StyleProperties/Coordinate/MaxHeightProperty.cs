using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Coordinate
{
    internal sealed class MaxHeightProperty : Property
    {
        private static readonly IValueConverter
            StyleConverter = Converters.OptionalLengthOrPercentConverter.OrDefault();

        internal MaxHeightProperty()
            : base(PropertyNames.MaxHeight, PropertyFlags.Animatable)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}