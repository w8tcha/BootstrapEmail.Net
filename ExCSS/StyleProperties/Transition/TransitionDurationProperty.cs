using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;
using ExCSS.Values;

namespace ExCSS.StyleProperties.Transition
{
    internal sealed class TransitionDurationProperty : Property
    {
        private static readonly IValueConverter
            ListConverter = Converters.TimeConverter.FromList().OrDefault(Time.Zero);

        internal TransitionDurationProperty()
            : base(PropertyNames.TransitionDuration)
        {
        }

        internal override IValueConverter Converter => ListConverter;
    }
}