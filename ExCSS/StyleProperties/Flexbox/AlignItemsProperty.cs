using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Flexbox
{
    internal sealed class AlignItemsProperty : Property
    {
        private static readonly IValueConverter StyleConverter = Converters.AlignItemsConverter
                                                                           .OrDefault(Keywords.Normal);

        internal AlignItemsProperty()
            : base(PropertyNames.AlignItems)
        { }

        internal override IValueConverter Converter => StyleConverter;
    }
}