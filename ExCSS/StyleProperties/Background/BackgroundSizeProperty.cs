using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Background
{
    internal sealed class BackgroundSizeProperty : Property
    {
        private static readonly IValueConverter ListConverter =
            Converters.BackgroundSizeConverter.FromList().OrDefault();

        internal BackgroundSizeProperty()
            : base(PropertyNames.BackgroundSize, PropertyFlags.Animatable)
        {
        }

        internal override IValueConverter Converter => ListConverter;
    }
}