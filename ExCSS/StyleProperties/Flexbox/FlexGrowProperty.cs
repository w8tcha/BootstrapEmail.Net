using ExCSS.Enumerations;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Flexbox
{
    internal sealed class FlexGrowProperty : Property
    {
        private static readonly IValueConverter StyleConverter = Converters.FlexGrowShrinkConverter;

        internal FlexGrowProperty()
            : base(PropertyNames.FlexGrow)
        { }

        internal override IValueConverter Converter => StyleConverter;
    }
}
