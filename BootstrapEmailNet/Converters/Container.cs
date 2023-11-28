namespace BootstrapEmail.Net.Converters;

public class Container : Base
{
    public Container(IHtmlDocument document, Config config)
        : base(document, config)
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