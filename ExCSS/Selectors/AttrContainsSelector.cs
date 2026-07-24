using ExCSS.Extensions;

namespace ExCSS.Selectors
{
    public sealed class AttrContainsSelector : AttrSelectorBase
    {
        public AttrContainsSelector(string attribute, string value) 
            : base(attribute, value, $"[{attribute}*={value.StylesheetString()}]")
        {
        }
    }
}