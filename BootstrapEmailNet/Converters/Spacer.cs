namespace BootstrapEmail.Net.Converters;

public class Spacer : Base
{
    public Spacer(IHtmlDocument document, Config config)
        : base(document, config)
    {
    }

    public virtual void Build()
    {
        foreach (var node in this.EachNode("*[class*=s-]"))
        {
            Dictionary<string, object> templateContent = new()
                                                             {
                                                                 { "classes", $"{node.ClassName} w-full" },
                                                                 { "contents", "&nbsp;" }
                                                             };

            node.OuterHtml = this.Template("table", templateContent);
        }
    }
}