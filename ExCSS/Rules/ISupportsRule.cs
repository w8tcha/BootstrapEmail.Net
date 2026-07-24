using ExCSS.Functions;

namespace ExCSS.Rules
{
    public interface ISupportsRule : IConditionRule
    {
        IConditionFunction Condition { get; }
    }
}