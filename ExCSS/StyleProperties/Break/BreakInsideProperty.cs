using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Break
{
    internal sealed class BreakInsideProperty : Property
    {
        private static readonly IValueConverter StyleConverter =
            Converters.BreakInsideModeConverter.OrDefault(BreakMode.Auto);

        internal BreakInsideProperty()
            : base(PropertyNames.BreakInside)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}