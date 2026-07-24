using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Sizing
{
    internal sealed class ObjectFitProperty : Property
    {
        private static readonly IValueConverter StyleConverter =
            Converters.ObjectFittingConverter.OrDefault(ObjectFitting.Fill);

        internal ObjectFitProperty()
            : base(PropertyNames.ObjectFit)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}