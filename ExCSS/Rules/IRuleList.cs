using System.Collections.Generic;

namespace ExCSS.Rules
{
    public interface IRuleList : IEnumerable<IRule>
    {
        IRule this[int index] { get; }
        int Length { get; }
    }
}