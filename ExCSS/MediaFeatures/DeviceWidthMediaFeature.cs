using ExCSS.Model;

namespace ExCSS.MediaFeatures
{
    internal sealed class DeviceWidthMediaFeature : MediaFeature
    {
        public DeviceWidthMediaFeature(string name) : base(name)
        {
        }

        internal override IValueConverter Converter => Converters.LengthConverter;
    }
}