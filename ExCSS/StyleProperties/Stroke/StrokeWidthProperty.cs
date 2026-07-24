using ExCSS.Enumerations;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Stroke
{
    internal sealed class StrokeWidthProperty : Property
    {
        private static readonly IValueConverter StyleConverter = Converters.LengthOrPercentConverter;

        internal StrokeWidthProperty()
            : base(PropertyNames.StrokeWidth, PropertyFlags.Animatable)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}