using ExCSS.Enumerations;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Text
{
    internal sealed class OverflowWrapProperty : Property
    {
        private static readonly IValueConverter StyleConverter = Converters.OverflowWrapConverter;

        public OverflowWrapProperty()
            : base(PropertyNames.OverflowWrap)
        {
        }

        internal override IValueConverter Converter => StyleConverter;
    }
}