namespace BootstrapEmail.Net.Converters;

public class Hr : Base
{
    public Hr(IHtmlDocument document)
        : base(document)
    {
    }

    public virtual void Build()
    {
        var parser = new HtmlParser();

        foreach (var node in this.EachNode("hr"))
        {
            var defaultMargin = this.IsMargin(node) ? string.Empty : "my-5" ;

            Dictionary<string, object> templateContent = new()
                                                             {
                                                                 { "classes", $"{defaultMargin} hr {node.ClassName}" },
                                                                 { "contents", "" }
                                                             };

            var table = parser.ParseDocument(this.Template("table", templateContent)).QuerySelector("table");

            node.ReplaceWith(table);
        }
    }
}