using ExCSS.Extensions;

namespace ExCSS.Selectors
{
    public sealed class AttrBeginsSelector : AttrSelectorBase
    {
        public AttrBeginsSelector(string attribute, string value) 
            : base(attribute, value, $"[{attribute}^={value.StylesheetString()}]")
        {
        }
    }
}