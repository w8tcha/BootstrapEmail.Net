namespace BootstrapEmail.Net.Converters;

public class Spacer : Base
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Spacer"/> class.
    /// </summary>
    /// <param name="document">The document.</param>
    /// <param name="config">The configuration.</param>
    /// <param name="context">the browsing context.</param>
    public Spacer(IHtmlDocument document, Config config, IBrowsingContext context)
        : base(document, config, context)
    {
    }

    public virtual void Build()
    {
        foreach (var node in this.EachNode("*[class*=s-]"))
        {
            //next unless node['class'].split.any? { |cls| cls.match?(/^s(-lg)?-\d+$/) }
            var templateContent = new TemplateContent($"{node.ClassName} w-full", "&nbsp;");

            node.OuterHtml = this.Template("table", templateContent);
        }
    }
}