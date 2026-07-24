using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;
using ExCSS.ValueConverters;

namespace ExCSS.StyleProperties.Box
{
    internal sealed class AspectRatioProperty : Property
    {
        private static readonly IValueConverter StyleConverter = new AspectRatioValueConverter().OrDefault();

        internal AspectRatioProperty()
            : base(PropertyNames.AspectRatio)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}
