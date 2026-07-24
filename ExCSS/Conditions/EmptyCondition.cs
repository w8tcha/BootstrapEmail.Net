using System.IO;

using ExCSS.Functions;
using ExCSS.Model;

namespace ExCSS.Conditions
{
    internal sealed class EmptyCondition : StylesheetNode, IConditionFunction
    {
        public bool Check()
        {
            return true;
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
        }
    }
}