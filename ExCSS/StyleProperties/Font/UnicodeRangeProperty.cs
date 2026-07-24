using ExCSS.Enumerations;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Font
{
    internal sealed class UnicodeRangeProperty : Property
    {
        public UnicodeRangeProperty()
            : base(PropertyNames.UnicodeRange)
        {
        }

        internal override IValueConverter Converter => Converters.Any;
    }
}