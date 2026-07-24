using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Animation
{
    internal sealed class AnimationIterationCountProperty : Property
    {
        private static readonly IValueConverter ListConverter =
            Converters.PositiveOrInfiniteNumberConverter.FromList().OrDefault(1f);

        internal AnimationIterationCountProperty()
            : base(PropertyNames.AnimationIterationCount)
        {
        }

        internal override IValueConverter Converter => ListConverter;
    }
}