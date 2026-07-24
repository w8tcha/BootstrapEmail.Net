using ExCSS.Model;

namespace ExCSS.Rules
{
    public interface IContainerRule : IConditionRule
    {
        string Name { get; set; }
        MediaList Media { get; }
  }
}