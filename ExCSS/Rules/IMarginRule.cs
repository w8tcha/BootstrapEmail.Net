using ExCSS.Model;

namespace ExCSS.Rules
{
    public interface IMarginRule : IRule
    {
        string Name { get; }
        StyleDeclaration Style { get; }
    }
}