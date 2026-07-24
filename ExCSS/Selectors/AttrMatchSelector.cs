using ExCSS.Extensions;

namespace ExCSS.Selectors
{
    public sealed class AttrMatchSelector : AttrSelectorBase
    {
        public AttrMatchSelector(string attribute, string value) 
            : base(attribute, value, $"[{attribute}={value.StylesheetString()}]")
        {
        }
    }
}