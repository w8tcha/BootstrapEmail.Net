using ExCSS.Enumerations;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Stroke
{
    internal sealed class StrokeDasharrayProperty : Property
    {
        private static readonly IValueConverter StyleConverter = Converters.StrokeDasharrayConverter;

        public StrokeDasharrayProperty()
            : base(PropertyNames.StrokeDasharray, PropertyFlags.Animatable | PropertyFlags.Unitless)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}