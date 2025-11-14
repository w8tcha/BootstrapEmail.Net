namespace BootstrapEmail.Net.Converters;

public class AlignDiv : Base
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Align"/> class.
    /// </summary>
    /// <param name="document">The document.</param>
    /// <param name="config">The configuration.</param>
    public AlignDiv(IHtmlDocument document, Config config)
        : base(document, config)
    {
    }

    public virtual void Build()
    {
        string[] types = ["start", "center", "end"];
        foreach (var type in types)
        {
            var fullType = $"text-{type}";

            foreach (var node in this.EachNode($".{fullType}:not(table):not(td)"))
            {
                this.AlignHelper(node, fullType, type);
            }
        }
    }

    public void AlignHelper(IElement node, string fullType, string type)
    {
        var parser = new HtmlParser();

        if (node.LocalName != "div")
        {
            return;
        }

        node.ClassList.Remove(fullType);

        var templateContent = new TemplateContent(fullType, node.ToHtml());

        var table = parser.ParseDocument(this.Template("table", templateContent)).QuerySelector("table");

        if (table == null)
        {
            return;
        }

        type = type switch
        {
            "start" => "left",
            "end" => "right",
            _ => type
        };

        table.SetAttribute("align", type);

        node.ReplaceWith(table);
    }
}