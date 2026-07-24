using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.MediaFeatures
{
    internal sealed class HoverMediaFeature : MediaFeature
    {
        private static readonly IValueConverter TheConverter = Map.HoverAbilities.ToConverter();

        public HoverMediaFeature() : base(FeatureNames.Hover)
        {
        }

        internal override IValueConverter Converter => TheConverter;
    }
}