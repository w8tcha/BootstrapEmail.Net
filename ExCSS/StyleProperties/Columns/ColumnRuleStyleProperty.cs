using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Columns
{
    internal sealed class ColumnRuleStyleProperty : Property
    {
        private static readonly IValueConverter
            StyleConverter = Converters.LineStyleConverter.OrDefault(LineStyle.None);

        internal ColumnRuleStyleProperty()
            : base(PropertyNames.ColumnRuleStyle)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}