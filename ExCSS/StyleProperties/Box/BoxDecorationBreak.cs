using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Box
{
    internal sealed class BoxDecorationBreak : Property
    {
        private static readonly IValueConverter StyleConverter = Converters.BoxDecorationConverter.OrDefault(false);

        internal BoxDecorationBreak()
            : base(PropertyNames.BoxDecorationBreak)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}