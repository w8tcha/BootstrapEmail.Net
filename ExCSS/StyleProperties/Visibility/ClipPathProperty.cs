using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;
using ExCSS.ValueConverters;

namespace ExCSS.StyleProperties.Visibility
{
    internal sealed class ClipPathProperty : Property
    {
        private static readonly IValueConverter StyleConverter = new ClipPathValueConverter().OrDefault();

        internal ClipPathProperty()
            : base(PropertyNames.ClipPath, PropertyFlags.Animatable)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}
