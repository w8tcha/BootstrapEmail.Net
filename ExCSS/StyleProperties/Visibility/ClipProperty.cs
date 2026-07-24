using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Visibility
{
    internal sealed class ClipProperty : Property
    {
        private static readonly IValueConverter StyleConverter = Converters.ShapeConverter.OrDefault();

        internal ClipProperty()
            : base(PropertyNames.Clip, PropertyFlags.Animatable)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}