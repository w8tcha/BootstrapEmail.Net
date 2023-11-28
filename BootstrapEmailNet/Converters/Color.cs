namespace BootstrapEmail.Net.Converters;

public class Color : Base
{
    public Color(IHtmlDocument document, Config config)
        : base(document, config)
    {
    }

    public virtual void Build()
    {
        foreach (var node in this.EachNode("*[class*=bg-]"))
        {
            if (!node.NodeName.Equals("div", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            var classes = node.ClassName;
            node.RemoveAttribute("class");

            var templateContent = new TemplateContent($"{classes} w-full", node.InnerHtml);

            node.OuterHtml = this.Template("table", templateContent);
        }
    }
}