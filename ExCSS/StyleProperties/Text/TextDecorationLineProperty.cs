using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Text
{
    internal sealed class TextDecorationLineProperty : Property
    {
        private static readonly IValueConverter ListConverter = Converters.TextDecorationLinesConverter.OrDefault();

        internal TextDecorationLineProperty()
            : base(PropertyNames.TextDecorationLine)
        {
        }

        internal override IValueConverter Converter => ListConverter;
    }
}