using ExCSS.Enumerations;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Flexbox
{
    internal sealed class FlexProperty : ShorthandProperty
    {
        private static readonly IValueConverter StyleConverter = Converters.FlexConverter;

        internal FlexProperty()
            : base(PropertyNames.Flex)
        { }

        internal override IValueConverter Converter => StyleConverter;
    }
}
