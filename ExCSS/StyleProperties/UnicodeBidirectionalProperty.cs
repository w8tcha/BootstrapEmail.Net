using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties
{
    internal sealed class UnicodeBidirectionalProperty : Property
    {
        private static readonly IValueConverter StyleConverter =
            Converters.UnicodeModeConverter.OrDefault(UnicodeMode.Normal);

        internal UnicodeBidirectionalProperty()
            : base(PropertyNames.UnicodeBidirectional)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}