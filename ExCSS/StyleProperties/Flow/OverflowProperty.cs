using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Flow
{
    internal sealed class OverflowProperty : Property
    {
        private static readonly IValueConverter StyleConverter =
            Converters.OverflowModeConverter.OrDefault(Overflow.Visible);

        internal OverflowProperty()
            : base(PropertyNames.Overflow)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}