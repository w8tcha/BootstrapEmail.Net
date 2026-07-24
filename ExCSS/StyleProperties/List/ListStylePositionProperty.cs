using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.List
{
    internal sealed class ListStylePositionProperty : Property
    {
        private static readonly IValueConverter StyleConverter =
            Converters.ListPositionConverter.OrDefault(ListPosition.Outside);

        internal ListStylePositionProperty()
            : base(PropertyNames.ListStylePosition, PropertyFlags.Inherited)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}