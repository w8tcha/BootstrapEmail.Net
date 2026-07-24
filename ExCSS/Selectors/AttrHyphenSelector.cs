using ExCSS.Extensions;

namespace ExCSS.Selectors
{
    public sealed class AttrHyphenSelector : AttrSelectorBase
    {
        public AttrHyphenSelector(string attribute, string value) 
            : base(attribute, value, $"[{attribute}|={value.StylesheetString()}]")
        {
        }
    }
}