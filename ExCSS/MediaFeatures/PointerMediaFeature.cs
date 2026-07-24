using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.MediaFeatures
{
    internal sealed class PointerMediaFeature : MediaFeature
    {
        private static readonly IValueConverter TheConverter = Map.PointerAccuracies.ToConverter();

        public PointerMediaFeature() : base(FeatureNames.Pointer)
        {
        }

        internal override IValueConverter Converter => TheConverter;
    }
}