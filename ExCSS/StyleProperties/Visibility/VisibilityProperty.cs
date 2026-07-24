using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Visibility
{
    internal sealed class VisibilityProperty : Property
    {
        private static readonly IValueConverter StyleConverter =
            Converters.VisibilityConverter.OrDefault(Enumerations.Visibility.Visible);

        internal VisibilityProperty()
            : base(PropertyNames.Visibility, PropertyFlags.Inherited | PropertyFlags.Animatable)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}