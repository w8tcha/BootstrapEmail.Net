namespace BootstrapEmail.Net.Converters;

public class Hr : Base
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Hr"/> class.
    /// </summary>
    /// <param name="document">The document.</param>
    /// <param name="config">The configuration.</param>
    public Hr(IHtmlDocument document, Config config, IBrowsingContext context)
        : base(document, config, context)
    {
    }

    public virtual void Build()
    {
        var parser = new HtmlParser();

        foreach (var node in this.EachNode("hr"))
        {
            if (!IsMargin(node))
            {
                node.ClassList.Add("my-5");
            }

            node.ClassList.Add("hr");

            var templateContent = new TemplateContent(node.ClassName!, string.Empty);

            var table = parser.ParseDocument(this.Template("table", templateContent)).QuerySelector("table");

            if (table != null)
            {
                node.ReplaceWith(table);
            }
        }
    }
}