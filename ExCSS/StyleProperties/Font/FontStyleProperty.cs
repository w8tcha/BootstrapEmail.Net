using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Font
{
    internal sealed class FontStyleProperty : Property
    {
        private static readonly IValueConverter StyleConverter =
            Converters.FontStyleConverter.OrDefault(FontStyle.Normal);

        internal FontStyleProperty()
            : base(PropertyNames.FontStyle, PropertyFlags.Inherited)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}