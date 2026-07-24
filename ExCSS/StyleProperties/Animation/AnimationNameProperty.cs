using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Animation
{
    internal sealed class AnimationNameProperty : Property
    {
        private static readonly IValueConverter ListConverter =
            Converters.IdentifierConverter.FromList().OrNone().OrDefault();

        internal AnimationNameProperty()
            : base(PropertyNames.AnimationName)
        {
        }

        internal override IValueConverter Converter => ListConverter;
    }
}