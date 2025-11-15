namespace BootstrapEmail.Net.Converters;

public class Container : Base
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Container"/> class.
    /// </summary>
    /// <param name="document">The document.</param>
    /// <param name="config">The configuration.</param>
    public Container(IHtmlDocument document, Config config, IBrowsingContext context)
        : base(document, config, context)
    {
    }

    public virtual void Build()
    {
        foreach (var node in this.EachNode(".container"))
        {
            var templateContent = new TemplateContent(node.ClassName ?? string.Empty, node.InnerHtml);

            node.OuterHtml = this.Template("container", templateContent);
        }

        foreach (var node in this.EachNode(".container-fluid"))
        {
            var templateContent = new TemplateContent(node.ClassName ?? string.Empty, node.InnerHtml);

            node.OuterHtml = this.Template("table", templateContent);
        }
    }
}