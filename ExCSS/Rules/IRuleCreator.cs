using ExCSS.Enumerations;

namespace ExCSS.Rules
{
    public interface IRuleCreator
    {
        IRule AddNewRule(RuleType ruleType);
    }
}