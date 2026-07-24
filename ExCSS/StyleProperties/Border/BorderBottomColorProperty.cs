using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;
using ExCSS.Values;

namespace ExCSS.StyleProperties.Border
{
    internal sealed class BorderBottomColorProperty : Property
    {
        private static readonly IValueConverter StyleConverter =
            Converters.CurrentColorConverter.OrDefault(Color.Transparent);

        internal BorderBottomColorProperty()
            : base(PropertyNames.BorderBottomColor)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}