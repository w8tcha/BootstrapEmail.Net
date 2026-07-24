using ExCSS.Model;

namespace ExCSS.Rules
{
    public interface IPageRule : IRule
    {
        string SelectorText { get; set; }
        StyleDeclaration Style { get; }
    }
}