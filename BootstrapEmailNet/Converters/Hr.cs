namespace BootstrapEmail.Net.Converters;

public class Hr : Base
{
    public Hr(IHtmlDocument document, Config config)
        : base(document, config)
    {
    }

    public virtual void Build()
    {
        var parser = new HtmlParser();

        foreach (var node in this.EachNode("hr"))
        {
            var defaultMargin = IsMargin(node) ? string.Empty : "my-5" ;

            Dictionary<string, object> templateContent = new()
                                                             {
                                                                 { "classes", $"{defaultMargin} hr {node.ClassName}" },
                                                                 { "contents", "" }
                                                             };

            var table = parser.ParseDocument(this.Template("table", templateContent)).QuerySelector("table");

            if (table != null)
            {
                node.ReplaceWith(table);
            }
        }
    }
}