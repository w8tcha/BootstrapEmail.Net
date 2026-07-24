using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Text
{
    internal sealed class TextDecorationStyleProperty : Property
    {
        private static readonly IValueConverter StyleConverter =
            Converters.TextDecorationStyleConverter.OrDefault(TextDecorationStyle.Solid);

        internal TextDecorationStyleProperty()
            : base(PropertyNames.TextDecorationStyle)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}