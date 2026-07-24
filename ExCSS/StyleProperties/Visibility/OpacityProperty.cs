using ExCSS.Enumerations;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Visibility
{
    internal sealed class OpacityProperty : Property
    {
        private static readonly IValueConverter StyleConverter = Converters.OptionalPercentOrNumberConverter;

        internal OpacityProperty() : base(PropertyNames.Opacity, PropertyFlags.Animatable)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}