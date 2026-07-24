using ExCSS.Enumerations;
using ExCSS.Model;

namespace ExCSS.MediaFeatures
{
    internal sealed class GridMediaFeature : MediaFeature
    {
        public GridMediaFeature() : base(FeatureNames.Grid)
        {
        }

        internal override IValueConverter Converter => Converters.BinaryConverter;
    }
}