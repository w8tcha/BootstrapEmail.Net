namespace BootstrapEmail.Net.Converters;

using AngleSharp.Html.Dom;

public class Padding : Base
{
    private static readonly string[] NodesNames = { "table", "td", "a" };

    public Padding(IHtmlDocument document, Config config)
        : base(document, config)
    {
    }

    public virtual void Build()
    {
        foreach (var node in this.EachNode(
                     "*[class^=p-], *[class^=pt-], *[class^=pr-], *[class^=pb-], *[class^=pl-], *[class^=px-], *[class^=py-], *[class*=' p-'], *[class*=' pt-'], *[class*=' pr-'], *[class*=' pb-'], *[class*=' pl-'], *[class*=' px-'], *[class*=' py-']"))
        {
            if (NodesNames.Contains(node.NodeName.ToLower()))
            {
                return;
            }

            var paddingRegex = new Regex("(p[trblxy]?-(lg-)?\\d+)", RegexOptions.None, TimeSpan.FromMilliseconds(100));
            var classes = paddingRegex.Replace(node.ClassName ?? string.Empty, string.Empty).Trim();
            node.ClassName = paddingRegex.Replace(node.ClassName ?? string.Empty, string.Empty).Trim();

            Dictionary<string, object> templateContent = new()
                                                             {
                                                                 { "classes", classes }, { "contents", node.OuterHtml }
                                                             };

            node.OuterHtml = this.Template("table", templateContent);
        }
    }
}