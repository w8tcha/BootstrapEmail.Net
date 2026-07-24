using ExCSS.Enumerations;

namespace ExCSS.Selectors

{
    public sealed class FirstChildSelector : ChildSelector
    {
        public FirstChildSelector()
            : base(PseudoClassNames.NthChild)
        {
        }
    }
}