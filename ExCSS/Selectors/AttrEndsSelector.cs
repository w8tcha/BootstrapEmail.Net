using ExCSS.Extensions;

namespace ExCSS.Selectors
{
    public sealed class AttrEndsSelector : AttrSelectorBase
    {
        public AttrEndsSelector(string attribute, string value) 
            : base(attribute, value, $"[{attribute}$={value.StylesheetString()}]")
        {
        }
    }
}