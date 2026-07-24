using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;
using ExCSS.Values;

namespace ExCSS.StyleProperties.Border
{
    internal sealed class BorderRightColorProperty : Property
    {
        private static readonly IValueConverter StyleConverter =
            Converters.CurrentColorConverter.OrDefault(Color.Transparent);

        internal BorderRightColorProperty()
            : base(PropertyNames.BorderRightColor)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}