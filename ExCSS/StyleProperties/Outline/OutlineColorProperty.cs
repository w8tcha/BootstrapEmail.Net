using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;
using ExCSS.Values;

namespace ExCSS.StyleProperties.Outline
{
    internal sealed class OutlineColorProperty : Property
    {
        private static readonly IValueConverter StyleConverter =
            Converters.InvertedColorConverter.OrDefault(Color.Transparent);

        internal OutlineColorProperty()
            : base(PropertyNames.OutlineColor, PropertyFlags.Animatable)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}