using ExCSS.Enumerations;
using ExCSS.Parser;

namespace ExCSS.Rules
{
    internal abstract class ConditionRule : GroupingRule
    {
        internal ConditionRule(RuleType type, StylesheetParser parser)
            : base(type, parser)
        {
        }
    }
}