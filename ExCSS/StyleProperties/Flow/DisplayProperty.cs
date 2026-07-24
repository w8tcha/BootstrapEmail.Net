using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Flow
{
    internal sealed class DisplayProperty : Property
    {
        private static readonly IValueConverter StyleConverter =
            Converters.DisplayModeConverter.OrDefault(DisplayMode.Inline);

        internal DisplayProperty()
            : base(PropertyNames.Display)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}