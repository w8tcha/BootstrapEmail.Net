using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;
using ExCSS.Values;

namespace ExCSS.StyleProperties.Font
{
    internal sealed class LineHeightProperty : Property
    {
        private static readonly IValueConverter StyleConverter =
            Converters.LineHeightConverter.OrDefault(new Length(120f, Length.Unit.Percent));

        internal LineHeightProperty()
            : base(PropertyNames.LineHeight, PropertyFlags.Inherited | PropertyFlags.Animatable)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}