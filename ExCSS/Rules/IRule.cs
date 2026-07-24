using ExCSS.Enumerations;
using ExCSS.Model;

namespace ExCSS.Rules
{
    public interface IRule : IStylesheetNode
    {
        RuleType Type { get; }
        string Text { get; set; }
        IRule Parent { get; }
        Stylesheet Owner { get; }
    }
}