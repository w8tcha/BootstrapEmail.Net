using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Coordinate
{
    internal sealed class MaxWidthProperty : Property
    {
        private static readonly IValueConverter
            StyleConverter = Converters.OptionalLengthOrPercentConverter.OrDefault();

        internal MaxWidthProperty()
            : base(PropertyNames.MaxWidth, PropertyFlags.Animatable)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}