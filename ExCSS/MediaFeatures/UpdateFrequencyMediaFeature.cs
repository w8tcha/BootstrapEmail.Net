using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.MediaFeatures
{
    internal sealed class UpdateFrequencyMediaFeature : MediaFeature
    {
        private static readonly IValueConverter TheConverter = Map.UpdateFrequencies.ToConverter();

        public UpdateFrequencyMediaFeature() : base(FeatureNames.UpdateFrequency)
        {
        }

        internal override IValueConverter Converter => TheConverter;
    }
}