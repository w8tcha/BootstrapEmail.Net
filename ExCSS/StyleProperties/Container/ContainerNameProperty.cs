using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Container
{
    internal sealed class ContainerNameProperty : Property
    {
        private static readonly IValueConverter StyleConverter =
            Converters.StringConverter.OrDefault();

        internal ContainerNameProperty()
            : base(PropertyNames.ContainerName)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}