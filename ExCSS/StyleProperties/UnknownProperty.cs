using ExCSS.Model;

namespace ExCSS.StyleProperties
{
    internal sealed class UnknownProperty : Property
    {
        internal UnknownProperty(string name)
            : base(name)
        {
        }

        internal override IValueConverter Converter => Converters.Any;
    }
}