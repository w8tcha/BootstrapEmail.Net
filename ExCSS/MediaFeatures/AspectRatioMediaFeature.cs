using ExCSS.Model;

namespace ExCSS.MediaFeatures
{
    internal sealed class AspectRatioMediaFeature : MediaFeature
    {
        public AspectRatioMediaFeature(string name) : base(name)
        {
        }

        internal override IValueConverter Converter => Converters.RatioConverter;
    }
}