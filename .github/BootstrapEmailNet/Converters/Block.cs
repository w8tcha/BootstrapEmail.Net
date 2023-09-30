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
            // add .to-table if it's not already there
            var classList = node.ClassName.Split().ToList();
            classList.Add("to-table");
            var className = string.Join(" ", classList.Distinct());

            Dictionary<string, object> templateContent = new()
                                                             {
                                                                 { "classes", className },
                                                                 { "contents", node.InnerHtml }
                                                             };

            node.OuterHtml = this.Template("table", templateContent);
        }
    }
}