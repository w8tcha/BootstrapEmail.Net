using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Background
{
    internal sealed class BackgroundImageProperty : Property
    {
        private static readonly IValueConverter StyleConverter = Converters.MultipleImageSourceConverter.OrDefault();

        internal BackgroundImageProperty()
            : base(PropertyNames.BackgroundImage)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}