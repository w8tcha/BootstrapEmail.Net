using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;
using ExCSS.Values;

namespace ExCSS.StyleProperties.Animation
{
    internal sealed class AnimationDelayProperty : Property
    {
        private static readonly IValueConverter
            ListConverter = Converters.TimeConverter.FromList().OrDefault(Time.Zero);

        internal AnimationDelayProperty()
            : base(PropertyNames.AnimationDelay)
        {
        }

        internal override IValueConverter Converter => ListConverter;
    }
}