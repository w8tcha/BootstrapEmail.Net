using ExCSS.Model;

namespace ExCSS.Rules
{
    public interface IStyleRule : IRule
    {
        string SelectorText { get; set; }
        StyleDeclaration Style { get; }
        ISelector Selector { get; set; }
    }
}