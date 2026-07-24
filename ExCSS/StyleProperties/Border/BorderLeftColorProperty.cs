using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;
using ExCSS.Values;

namespace ExCSS.StyleProperties.Border
{
    internal sealed class BorderLeftColorProperty : Property
    {
        private static readonly IValueConverter StyleConverter =
            Converters.CurrentColorConverter.OrDefault(Color.Transparent);

        internal BorderLeftColorProperty()
            : base(PropertyNames.BorderLeftColor)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}