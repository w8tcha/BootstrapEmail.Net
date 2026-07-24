using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.BorderImage
{
    internal sealed class BorderImageSourceProperty : Property
    {
        private static readonly IValueConverter StyleConverter = Converters.OptionalImageSourceConverter.OrDefault();

        internal BorderImageSourceProperty()
            : base(PropertyNames.BorderImageSource)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}