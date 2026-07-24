using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;
using ExCSS.Values;

namespace ExCSS.StyleProperties.BorderImage
{
    internal sealed class BorderImageOutsetProperty : Property
    {
        internal static readonly IValueConverter TheConverter = Converters.LengthOrPercentConverter.Periodic();
        private static readonly IValueConverter StyleConverter = TheConverter.OrDefault(Length.Zero);

        internal BorderImageOutsetProperty()
            : base(PropertyNames.BorderImageOutset)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}