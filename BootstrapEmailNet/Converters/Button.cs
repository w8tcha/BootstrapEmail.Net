namespace BootstrapEmail.Net.Converters;

public class Button : Base
{
    public Button(IHtmlDocument document)
        : base(document)
    {
    }

    public virtual void Build()
    {
        foreach (var node in this.EachNode(".btn"))
        {
            var className = node.ClassName ?? string.Empty; ;

            node.RemoveAttribute("class");

            Dictionary<string, object> templateContent = new()
                                                             {
                                                                 { "classes", className },
                                                                 {
                                                                     "contents",
                                                                     node.OuterHtml
                                                                 }
                                                             };

             node.OuterHtml = this.Template("button", templateContent);
        }
    }
}