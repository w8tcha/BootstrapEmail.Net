namespace BootstrapEmail.Net.Converters;

public class Alert : Base
{
    public Alert(IHtmlDocument document, Config config)
        : base(document, config)
    {
    }

    public virtual void Build()
    {
        foreach (var node in this.EachNode(".alert"))
        {
            var classes = node.ClassName ?? string.Empty;
            node.RemoveAttribute("class");

            Dictionary<string, object> templateContent = new()
                                                             {
                                                                 { "classes", classes },
                                                                 { "contents", node.OuterHtml }
                                                             };
            
            node.OuterHtml = this.Template("table", templateContent);
        }
    }
}