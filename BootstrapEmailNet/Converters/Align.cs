namespace BootstrapEmail.Net.Converters;

public class Align : Base
{
    public Align(IHtmlDocument document, Config config)
        : base(document, config)
    {
    }

    public virtual void Build()
    {
        string[] types = { "left", "center", "right" };
        foreach (var type in types)
        {
            var fullType = $"ax-{type}";

            foreach (var node in this.EachNode($".{fullType}"))
            {
                this.AlignHelper(node, fullType, type);
            }
        }
    }

    public void AlignHelper(IElement node, string fullType, string type)
    {
        var parser = new HtmlParser();

        if (this.IsTable(node) || this.IsTd(node))
        {
            return;
        }

        node.ClassName = node.ClassName?.Replace(fullType, string.Empty).Trim();

        Dictionary<string, object> templateContent = new()
                                                         {
                                                             { "classes", fullType },
                                                             {
                                                                 "contents",
                                                                 node.ToHtml()
                                                             }
                                                         };

        var table = parser.ParseDocument(this.Template("table", templateContent)).QuerySelector("table");

        if (table == null)
        {
            return;
        }

        table.SetAttribute("align", type);

        node.ReplaceWith(table);
    }
}