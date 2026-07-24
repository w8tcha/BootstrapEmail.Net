using ExCSS.Model;

namespace ExCSS.Rules
{
    public interface IMediaRule : IConditionRule
    {
        MediaList Media { get; }
    }
}