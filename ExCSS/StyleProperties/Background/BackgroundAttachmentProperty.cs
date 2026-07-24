using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties.Background
{
    internal sealed class BackgroundAttachmentProperty : Property
    {
        private static readonly IValueConverter AttachmentConverter =
            Converters.BackgroundAttachmentConverter.FromList().OrDefault(BackgroundAttachment.Scroll);

        internal BackgroundAttachmentProperty()
            : base(PropertyNames.BackgroundAttachment)
        {
        }

        internal override IValueConverter Converter => AttachmentConverter;
    }
}