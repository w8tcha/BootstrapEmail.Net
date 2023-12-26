namespace BootstrapEmail.Net.Converters;

using AngleSharp.Html.Dom;

public class Padding : Base
{
    private static readonly string[] NodesNames = ["table", "td", "a"];

    /// <summary>
    /// Initializes a new instance of the <see cref="Padding"/> class.
    /// </summary>
    /// <param name="document">The document.</param>
    /// <param name="config">The configuration.</param>
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

            if (string.IsNullOrEmpty(node.ClassName))
            {
                continue;
            }

            var paddingRegex = new Regex("(p[trblxy]?-(lg-)?\\d+)", RegexOptions.None, TimeSpan.FromMilliseconds(100));

            node.ClassName = paddingRegex.Replace(node.ClassName, string.Empty).Trim();

            var templateContent = new TemplateContent(node.ClassName, node.OuterHtml);

            node.OuterHtml = this.Template("table", templateContent);
        }
    }
}