using ExCSS.Enumerations;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Flexbox
{
    internal sealed class AlignSelfProperty : Property
    {
        private static readonly IValueConverter StyleConverter = Converters.AlignSelfConverter;

        internal AlignSelfProperty()
            : base(PropertyNames.AlignSelf)
        { }

        internal override IValueConverter Converter => StyleConverter;
    }
}
