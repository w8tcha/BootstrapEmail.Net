using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.List
{
    internal sealed class ListStyleTypeProperty : Property
    {
        private static readonly IValueConverter
            StyleConverter = Converters.ListStyleConverter.OrDefault(ListStyle.Disc);

        internal ListStyleTypeProperty()
            : base(PropertyNames.ListStyleType, PropertyFlags.Inherited)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}