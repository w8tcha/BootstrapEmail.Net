using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;
using ExCSS.Values;

namespace ExCSS.StyleProperties.Sizing
{
    internal sealed class ObjectPositionProperty : Property
    {
        private static readonly IValueConverter StyleConverter = Converters.PointConverter.OrDefault(Point.Center);

        internal ObjectPositionProperty()
            : base(PropertyNames.ObjectPosition, PropertyFlags.Animatable)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}