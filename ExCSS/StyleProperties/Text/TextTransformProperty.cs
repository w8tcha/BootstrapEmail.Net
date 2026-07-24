using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Text
{
    internal sealed class TextTransformProperty : Property
    {
        private static readonly IValueConverter StyleConverter =
            Converters.TextTransformConverter.OrDefault(TextTransform.None);

        internal TextTransformProperty()
            : base(PropertyNames.TextTransform, PropertyFlags.Inherited)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}