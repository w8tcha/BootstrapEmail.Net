using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;
using ExCSS.Values;

namespace ExCSS.StyleProperties.BorderImage
{
    internal sealed class BorderImageWidthProperty : Property
    {
        internal static readonly IValueConverter TheConverter = Converters.ImageBorderWidthConverter.Periodic();
        private static readonly IValueConverter StyleConverter = TheConverter.OrDefault(Length.Full);

        internal BorderImageWidthProperty()
            : base(PropertyNames.BorderImageWidth)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}