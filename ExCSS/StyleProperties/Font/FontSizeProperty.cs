using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Font
{
    internal sealed class FontSizeProperty : Property
    {
        private static readonly IValueConverter StyleConverter =
            Converters.FontSizeConverter.OrDefault(FontSize.Medium.ToLength());

        internal FontSizeProperty()
            : base(PropertyNames.FontSize, PropertyFlags.Inherited | PropertyFlags.Unitless | PropertyFlags.Animatable)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}