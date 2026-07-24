using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Flow
{
    internal sealed class ClearProperty : Property
    {
        private static readonly IValueConverter
            StyleConverter = Converters.ClearModeConverter.OrDefault(ClearMode.None);

        internal ClearProperty()
            : base(PropertyNames.Clear)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}