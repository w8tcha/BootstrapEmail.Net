using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.BorderImage
{
    using static Converters;

    internal sealed class BorderImageProperty : ShorthandProperty
    {
        private static readonly IValueConverter ImageConverter = WithAny(
            OptionalImageSourceConverter.Option().For(PropertyNames.BorderImageSource),
            WithOrder(
                BorderImageSliceProperty.TheConverter.Option().For(PropertyNames.BorderImageSlice),
                BorderImageWidthProperty.TheConverter.StartsWithDelimiter()
                    .Option()
                    .For(PropertyNames.BorderImageWidth),
                BorderImageOutsetProperty.TheConverter.StartsWithDelimiter()
                    .Option()
                    .For(PropertyNames.BorderImageOutset)),
            BorderImageRepeatProperty.TheConverter.Option().For(PropertyNames.BorderImageRepeat)).OrDefault();

        internal BorderImageProperty()
            : base(PropertyNames.BorderImage)
        {
        }

        internal override IValueConverter Converter => ImageConverter;
    }
}