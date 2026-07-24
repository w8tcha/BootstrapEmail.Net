using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Border
{
    internal sealed class BorderRightStyleProperty : Property
    {
        private static readonly IValueConverter
            StyleConverter = Converters.LineStyleConverter.OrDefault(LineStyle.None);

        internal BorderRightStyleProperty()
            : base(PropertyNames.BorderRightStyle)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}