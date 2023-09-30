namespace BootstrapEmail.Net.Converters;

public class Block : Base
{
    public Block(IHtmlDocument document)
        : base(document)
    {
    }

    public virtual void Build()
    {
        foreach (var node in this.EachNode("block, .to-table"))
        {
            var className = node.ClassName ?? string.Empty;

            // add .to-table if it's not already there
            var classList = className.Split().ToList();
            classList.Add("to-table");

            Dictionary<string, object> templateContent = new()
                                                             {
                                                                 { "classes", string.Join(" ", classList.Distinct()) },
                                                                 { "contents", node.InnerHtml }
                                                             };

            node.OuterHtml = this.Template("table", templateContent);
        }
    }
}