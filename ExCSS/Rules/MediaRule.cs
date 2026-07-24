using System.IO;
using System.Linq;

using ExCSS.Enumerations;
using ExCSS.Model;
using ExCSS.Parser;

namespace ExCSS.Rules
{
    internal sealed class MediaRule : ConditionRule, IMediaRule
    {
        internal MediaRule(StylesheetParser parser) : base(RuleType.Media, parser)
        {
            AppendChild(new MediaList(parser));
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var rules = formatter.Block(Rules);
            writer.Write(formatter.Rule("@media", Media.MediaText, rules));
        }

        public string ConditionText
        {
            get => Media.MediaText;
            set => Media.MediaText = value;
        }

        public MediaList Media => Children.OfType<MediaList>().FirstOrDefault();
    }
}