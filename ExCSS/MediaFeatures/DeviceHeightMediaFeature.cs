using ExCSS.Model;

namespace ExCSS.MediaFeatures
{
    internal sealed class DeviceHeightMediaFeature : MediaFeature
    {
        public DeviceHeightMediaFeature(string name) : base(name)
        {
        }

        internal override IValueConverter Converter => Converters.LengthConverter;
    }
}