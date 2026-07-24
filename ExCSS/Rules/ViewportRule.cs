using ExCSS.Enumerations;
using ExCSS.Factories;
using ExCSS.Parser;
using ExCSS.StyleProperties;

namespace ExCSS.Rules
{
    internal sealed class ViewportRule : DeclarationRule
    {
        internal ViewportRule(StylesheetParser parser)
            : base(RuleType.Viewport, RuleNames.ViewPort, parser)
        {
        }

        protected override Property CreateNewProperty(string name)
        {
            return PropertyFactory.Instance.CreateViewport(name);
        }
    }
}