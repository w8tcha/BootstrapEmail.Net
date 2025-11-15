namespace BootstrapEmail.Net.Converters;

using System.Text;

public class Grid : Base
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Grid"/> class.
    /// </summary>
    /// <param name="document">The document.</param>
    /// <param name="config">The configuration.</param>
    public Grid(IHtmlDocument document, Config config, IBrowsingContext context)
        : base(document, config, context)
    {
    }

    public virtual void Build()
    {
        foreach (var node in this.EachNode(".row"))
        {
            // Parse columns first
            var innerHtml = this.ParseColumns(node);

            var tableToTr = new TemplateContent(node.ClassName ?? string.Empty, innerHtml);

            var tableToTrContent = this.Template("table-to-tr", tableToTr);

            if (node.QuerySelector("*[class*=col-lg-]") != null)
            {
                AddClass(node, "row-responsive");
            }

            var templateContent = new TemplateContent(node.ClassName ?? string.Empty, tableToTrContent);

            node.OuterHtml = this.Template("div", templateContent);
        }
    }

    private string ParseColumns(IElement element)
    {
        var parsedHtml = new StringBuilder();

        foreach (var templateContent in EachChildNode(element, "*[class*=col]")
                     .Select(node => new TemplateContent(node.ClassName ?? string.Empty, node.InnerHtml)))
        {
            parsedHtml.Append(this.Template("td", templateContent));
        }

        return parsedHtml.ToString();
    }
}