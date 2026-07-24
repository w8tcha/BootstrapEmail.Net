using ExCSS.Enumerations;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Flexbox
{
    internal sealed class FlexDirectionProperty : Property
    {
        private static readonly IValueConverter StyleConverter = Converters.FlexDirectionConverter;

        internal FlexDirectionProperty()
            : base(PropertyNames.FlexDirection)
        { }

        internal override IValueConverter Converter => StyleConverter;
    }
}