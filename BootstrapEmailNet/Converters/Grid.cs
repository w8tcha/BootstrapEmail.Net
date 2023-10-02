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

            Dictionary<string, object> tableToTr = new() { { "classes", node.ClassName ?? string.Empty },
                                                             { "contents", innerHtml } };

            var tableToTrContent = this.Template("table-to-tr", tableToTr);

            Dictionary<string, object> templateContent = new()
                                                             {
                                                                 { "classes", node.ClassName ?? string.Empty },
                                                                 { "contents", tableToTrContent }
                                                             };

            if (node.QuerySelector("*[class*=col-lg-]") != null)
            {
                this.AddClass(node, "row-responsive");
            }

            node.OuterHtml = this.Template("div", templateContent);
        }
    }

    private string ParseColumns(IElement element)
    {
        var parsedHtml = new StringBuilder();

        foreach (var templateContent in this.EachChildNode(element, "*[class*=col]").Select(node => new Dictionary<string, object>
                     {
                         { "classes", node.ClassName ?? string.Empty },
                         { "contents", node.InnerHtml }
                     }))
        {
            parsedHtml.Append(this.Template("td", templateContent));
        }

        return parsedHtml.ToString();
    }
}