using ExCSS.Enumerations;

namespace ExCSS.Selectors
{
    public sealed class LastColumnSelector : ChildSelector
    {
        public LastColumnSelector()
            : base(PseudoClassNames.NthLastColumn)
        {
        }
    }
}