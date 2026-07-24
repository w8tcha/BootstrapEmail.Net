using ExCSS.Enumerations;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Flexbox
{
    internal sealed class FlexFlowProperty : ShorthandProperty
    {
        private static readonly IValueConverter StyleConverter = Converters.FlexFlowConverter;

        internal FlexFlowProperty()
            : base(PropertyNames.FlexFlow)
        { }

        internal override IValueConverter Converter => StyleConverter;
    }
}
