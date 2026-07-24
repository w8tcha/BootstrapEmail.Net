using ExCSS.Enumerations;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Fill
{
    internal sealed class FillProperty : Property
    {
        private static readonly IValueConverter StyleConverter = Converters.PaintConverter;

        internal FillProperty()
            : base(PropertyNames.Fill, PropertyFlags.Animatable)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}