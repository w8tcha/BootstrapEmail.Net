using ExCSS.Enumerations;

namespace ExCSS.Selectors
{
    public sealed class LastTypeSelector : ChildSelector
    {
        public LastTypeSelector()
            : base(PseudoClassNames.NthLastOfType)
        {
        }
    }
}