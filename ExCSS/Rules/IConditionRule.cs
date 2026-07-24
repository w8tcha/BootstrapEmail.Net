namespace ExCSS.Rules
{
    public interface IConditionRule : IGroupingRule
    {
        string ConditionText { get; set; }
    }
}