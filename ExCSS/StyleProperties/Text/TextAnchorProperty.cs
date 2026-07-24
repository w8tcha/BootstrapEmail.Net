using ExCSS.Enumerations;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Text
{
    internal sealed class TextAnchorProperty : Property
    {
        private static readonly IValueConverter StyleConverter = Converters.TextAnchorConverter;

        public TextAnchorProperty()
            : base(PropertyNames.TextAnchor)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}