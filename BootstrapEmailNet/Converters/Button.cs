namespace BootstrapEmail.Net.Converters;

public class Button : Base
{
    public Button(IHtmlDocument document, Config config)
        : base(document, config)
    {
    }

    public virtual void Build()
    {
        foreach (var node in this.EachNode(".btn"))
        {
            var className = node.ClassName ?? string.Empty;

            node.RemoveAttribute("class");

            var templateContent = new TemplateContent(className, node.OuterHtml);

            node.OuterHtml = this.Template("button", templateContent);
        }
    }
}