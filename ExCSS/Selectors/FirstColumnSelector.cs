using ExCSS.Enumerations;

namespace ExCSS.Selectors
{
    public sealed class FirstColumnSelector : ChildSelector
    {
        public FirstColumnSelector()
            : base(PseudoClassNames.NthColumn)
        {
        }
    }
}