using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.MediaFeatures
{
    using static Converters;

    internal sealed class ColorIndexMediaFeature : MediaFeature
    {
        public ColorIndexMediaFeature(string name) : base(name)
        {
        }

        internal override IValueConverter Converter =>
            IsMinimum || IsMaximum
                ? NaturalIntegerConverter
                : NaturalIntegerConverter.Option(1);
    }
}