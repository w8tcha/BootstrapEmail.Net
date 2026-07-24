using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Animation
{
    internal sealed class AnimationPlayStateProperty : Property
    {
        private static readonly IValueConverter ListConverter =
            Converters.PlayStateConverter.FromList().OrDefault(PlayState.Running);

        internal AnimationPlayStateProperty()
            : base(PropertyNames.AnimationPlayState)
        {
        }

        internal override IValueConverter Converter => ListConverter;
    }
}