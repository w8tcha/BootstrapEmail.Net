using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Break
{
    internal sealed class PageBreakAfterProperty : Property
    {
        private static readonly IValueConverter StyleConverter =
            Converters.PageBreakModeConverter.OrDefault(BreakMode.Auto);

        internal PageBreakAfterProperty()
            : base(PropertyNames.PageBreakAfter)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}