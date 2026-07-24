using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Break
{
    internal sealed class BreakBeforeProperty : Property
    {
        private static readonly IValueConverter
            StyleConverter = Converters.BreakModeConverter.OrDefault(BreakMode.Auto);

        internal BreakBeforeProperty()
            : base(PropertyNames.BreakBefore)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}