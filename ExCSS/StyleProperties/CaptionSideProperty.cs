using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties
{
    internal sealed class CaptionSideProperty : Property
    {
        private static readonly IValueConverter StyleConverter = Converters.CaptionSideConverter.OrDefault(true);

        internal CaptionSideProperty() : base(PropertyNames.CaptionSide)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}