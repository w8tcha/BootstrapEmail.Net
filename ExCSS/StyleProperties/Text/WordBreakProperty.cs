using ExCSS.Enumerations;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Text
{
    internal sealed class WordBreakProperty : Property
    {
        private static readonly IValueConverter StyleConverter = Converters.WordBreakConverter;

        public WordBreakProperty()
            : base(PropertyNames.WordBreak)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}