using ExCSS.Enumerations;
using ExCSS.Extensions;
using ExCSS.Model;

namespace ExCSS.StyleProperties
{
    internal sealed class RowGapProperty : Property
    {
        private static readonly IValueConverter StyleConverter = Converters.LengthOrPercentConverter
                                                                           .OrGlobalValue()
                                                                           .OrDefault(0);

        internal RowGapProperty()
            : base(PropertyNames.RowGap, PropertyFlags.Animatable)
        { }

        internal override IValueConverter Converter => StyleConverter;
    }
}
