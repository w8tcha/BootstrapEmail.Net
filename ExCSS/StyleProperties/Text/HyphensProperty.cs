using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Text
{
    internal sealed class HyphensProperty : Property
    {
        private static readonly IValueConverter StyleConverter =
            Converters.HyphensConverter.OrDefault(Hyphens.Manual);

        internal HyphensProperty()
            : base(PropertyNames.Hyphens, PropertyFlags.Inherited)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}
