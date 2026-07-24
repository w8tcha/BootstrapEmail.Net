using ExCSS.Model;
using ExCSS.Selectors;

namespace ExCSS.Rules
{
    public interface IKeyframeRule : IRule
    {
        string KeyText { get; set; }
        StyleDeclaration Style { get; }
        KeyframeSelector Key { get; set; }
    }
}