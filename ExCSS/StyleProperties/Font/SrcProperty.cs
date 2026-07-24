using ExCSS.Enumerations;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Font
{
    internal sealed class SrcProperty : Property
    {
        public SrcProperty()
            : base(PropertyNames.Src)
        {
        }

        internal override IValueConverter Converter => Converters.Any;
    }
}