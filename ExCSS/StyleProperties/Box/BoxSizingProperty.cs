using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Box
{
    internal class BoxSizingProperty : Property
    {
        private static readonly IValueConverter StyleConverter = Converters.BoxSizingConverter.OrDefault(Keywords.ContentBox);

        public BoxSizingProperty() 
            : base(PropertyNames.BoxSizing, PropertyFlags.None)
        { }

        internal override IValueConverter Converter => StyleConverter;
    }
}
