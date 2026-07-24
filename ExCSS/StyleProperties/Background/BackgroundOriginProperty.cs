using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Background
{
    internal sealed class BackgroundOriginProperty : Property
    {
        private static readonly IValueConverter ListConverter =
            Converters.BoxModelConverter.FromList().OrDefault(BoxModel.PaddingBox);

        internal BackgroundOriginProperty()
            : base(PropertyNames.BackgroundOrigin)
        {
        }

        internal override IValueConverter Converter => ListConverter;
    }
}