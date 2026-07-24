using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Transform
{
    internal sealed class TransformProperty : Property
    {
        private static readonly IValueConverter StyleConverter =
            Converters.TransformConverter.Many().OrNone().OrDefault();

        internal TransformProperty()
            : base(PropertyNames.Transform, PropertyFlags.Animatable)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}