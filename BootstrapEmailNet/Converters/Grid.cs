namespace BootstrapEmail.Net.Converters;

using System.Text;

public class Grid : Base
{
    public Grid(IHtmlDocument document, Config config)
        : base(document, config)
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