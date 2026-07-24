using System.IO;

using ExCSS.Enumerations;
using ExCSS.Model;
using ExCSS.Parser;

namespace ExCSS.Rules
{
    internal sealed class UnknownRule : Rule
    {
        public UnknownRule(string name, StylesheetParser parser)
            : base(RuleType.Unknown, parser)
        {
            Name = name;
        }

        public string Name { get; }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write(StylesheetText?.Text);
        }
    }
}