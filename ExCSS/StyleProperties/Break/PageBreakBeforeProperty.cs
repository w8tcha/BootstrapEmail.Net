using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Break
{
    internal sealed class PageBreakBeforeProperty : Property
    {
        private static readonly IValueConverter StyleConverter =
            Converters.PageBreakModeConverter.OrDefault(BreakMode.Auto);

        internal PageBreakBeforeProperty()
            : base(PropertyNames.PageBreakBefore)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}