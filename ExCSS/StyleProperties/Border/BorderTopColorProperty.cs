using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;
using ExCSS.Values;

namespace ExCSS.StyleProperties.Border
{
    internal sealed class BorderTopColorProperty : Property
    {
        private static readonly IValueConverter StyleConverter =
            Converters.CurrentColorConverter.OrDefault(Color.Transparent);

        internal BorderTopColorProperty()
            : base(PropertyNames.BorderTopColor)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}