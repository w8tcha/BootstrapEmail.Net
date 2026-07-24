using ExCSS.Model;

namespace ExCSS.MediaFeatures
{
    internal sealed class WidthMediaFeature : MediaFeature
    {
        public WidthMediaFeature(string name) : base(name)
        {
        }

        internal override IValueConverter Converter => Converters.LengthConverter;
    }
}