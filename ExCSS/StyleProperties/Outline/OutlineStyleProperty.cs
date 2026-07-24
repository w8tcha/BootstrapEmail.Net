using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Outline
{
    internal sealed class OutlineStyleProperty : Property
    {
        private static readonly IValueConverter
            StyleConverter = Converters.LineStyleConverter.OrDefault(LineStyle.None);

        internal OutlineStyleProperty()
            : base(PropertyNames.OutlineStyle)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}