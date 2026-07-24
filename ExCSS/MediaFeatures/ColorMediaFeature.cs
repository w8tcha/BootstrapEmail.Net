using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.MediaFeatures
{
    using static Converters;

    internal sealed class ColorMediaFeature : MediaFeature
    {
        public ColorMediaFeature(string name) : base(name)
        {
        }

        internal override IValueConverter Converter =>
            IsMinimum || IsMaximum
                ? PositiveIntegerConverter
                : PositiveIntegerConverter.Option(1);
    }
}