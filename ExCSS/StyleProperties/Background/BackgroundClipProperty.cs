using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Background
{
    internal sealed class BackgroundClipProperty : Property
    {
        private static readonly IValueConverter ListConverter =
            Converters.BoxModelConverter.FromList().OrDefault(BoxModel.BorderBox);

        internal BackgroundClipProperty()
            : base(PropertyNames.BackgroundClip)
        {
        }

        internal override IValueConverter Converter => ListConverter;
    }
}