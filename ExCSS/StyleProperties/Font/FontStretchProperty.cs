using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Font
{
    internal sealed class FontStretchProperty : Property
    {
        private static readonly IValueConverter StyleConverter =
            Converters.FontStretchConverter.OrDefault(FontStretch.Normal);

        internal FontStretchProperty()
            : base(PropertyNames.FontStretch, PropertyFlags.Inherited | PropertyFlags.Animatable)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}