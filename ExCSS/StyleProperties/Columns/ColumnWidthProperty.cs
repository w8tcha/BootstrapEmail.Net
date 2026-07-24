using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Columns
{
    internal sealed class ColumnWidthProperty : Property
    {
        private static readonly IValueConverter
            StyleConverter = Converters.AutoLengthConverter.OrDefault(Keywords.Auto);

        internal ColumnWidthProperty()
            : base(PropertyNames.ColumnWidth, PropertyFlags.Animatable)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}