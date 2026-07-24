using ExCSS.Model;

namespace ExCSS.Functions
{
    public interface IConditionFunction : IStylesheetNode
    {
        bool Check();
    }
}