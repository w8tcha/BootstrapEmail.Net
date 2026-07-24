using ExCSS.Model;

namespace ExCSS.Rules
{
    public interface IImportRule : IRule
    {
        string Href { get; set; }
        MediaList Media { get; }
    }
}