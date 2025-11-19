namespace BootstrapEmail.Net.Converters;

public class Badge : Base
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Badge"/> class.
    /// </summary>
    /// <param name="document">The document.</param>
    /// <param name="config">The configuration.</param>
    /// <param name="context">the browsing context.</param>
    public Badge(IHtmlDocument document, Config config, IBrowsingContext context)
        : base(document, config, context)
    {
    }

    public virtual void Build()
    {
        foreach (var node in this.EachNode(".badge"))
        {
            var classes = node.ClassName ?? string.Empty;
            node.RemoveAttribute("class");

            var templateContent = new TemplateContent(classes, node.OuterHtml);

            node.OuterHtml = this.Template("table-left", templateContent);
        }
    }
}