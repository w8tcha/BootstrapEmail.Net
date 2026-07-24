using ExCSS.Extensions;

namespace ExCSS.Selectors
{
    public sealed class AttrNotMatchSelector : AttrSelectorBase
    {
        public AttrNotMatchSelector(string attribute, string value) 
            : base(attribute, value, $"[{attribute}!={value.StylesheetString()}]")
        {
        }
    }
}