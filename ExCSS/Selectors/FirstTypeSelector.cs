using ExCSS.Enumerations;

namespace ExCSS.Selectors
{
    public sealed class FirstTypeSelector : ChildSelector
    {
        public FirstTypeSelector()
            : base(PseudoClassNames.NthOfType)
        {
        }
    }
}