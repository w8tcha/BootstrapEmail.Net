using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;
using ExCSS.Values;

namespace ExCSS.StyleProperties.Background
{
    internal sealed class BackgroundPositionProperty : Property
    {
        private static readonly IValueConverter ListConverter =
            Converters.PointConverter.FromList().OrDefault(Point.Center);

        internal BackgroundPositionProperty()
            : base(PropertyNames.BackgroundPosition, PropertyFlags.Animatable)
        {
        }

        internal override IValueConverter Converter => ListConverter;
    }
}