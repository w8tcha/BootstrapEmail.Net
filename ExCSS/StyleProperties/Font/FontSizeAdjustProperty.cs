using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Font
{
    internal sealed class FontSizeAdjustProperty : Property
    {
        private static readonly IValueConverter StyleConverter = Converters.OptionalNumberConverter.OrDefault();

        internal FontSizeAdjustProperty()
            : base(PropertyNames.FontSizeAdjust, PropertyFlags.Inherited | PropertyFlags.Animatable)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}