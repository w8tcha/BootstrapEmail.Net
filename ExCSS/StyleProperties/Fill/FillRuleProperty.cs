using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Fill
{
    internal sealed class FillRuleProperty : Property
    {
        private static readonly IValueConverter StyleConverter =
            Converters.FillRuleConverter.OrDefault(FillRule.Nonzero);

        public FillRuleProperty()
            : base(PropertyNames.FillRule, PropertyFlags.Animatable)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}