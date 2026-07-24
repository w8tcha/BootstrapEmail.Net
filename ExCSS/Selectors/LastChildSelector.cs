using ExCSS.Enumerations;

namespace ExCSS.Selectors
{
    public sealed class LastChildSelector : ChildSelector
    {
        public LastChildSelector()
            : base(PseudoClassNames.NthLastChild)
        {
        }
    }
}