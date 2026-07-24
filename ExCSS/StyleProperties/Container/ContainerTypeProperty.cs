using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Container
{
    internal sealed class ContainerTypeProperty : Property
    {
        private static readonly IValueConverter StyleConverter =
            Converters.ContainerTypeConverter.OrDefault(Keywords.Normal);

        internal ContainerTypeProperty()
            : base(PropertyNames.ContainerType)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}