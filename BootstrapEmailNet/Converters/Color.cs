namespace BootstrapEmail.Net.Converters;

public class Color : Base
{
    public Color(IHtmlDocument document)
        : base(document)
    {
    }

    public virtual void Build()
    {
        foreach (var node in this.EachNode("*[class*=bg-]"))
        {
            if (node.NodeName.ToLower() != "div")
            {
                continue;
            }

            var classes = node.ClassName;
            node.RemoveAttribute("class");

            Dictionary<string, object> templateContent = new()
                                                             {
                                                                 { "classes", $"{classes} w-full" },
                                                                 { "contents", node.InnerHtml }
                                                             };

            node.OuterHtml = this.Template("table", templateContent);
        }
    }
}