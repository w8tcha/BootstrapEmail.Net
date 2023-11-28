namespace BootstrapEmail.Net.Converters;

public class Block : Base
{
    public Block(IHtmlDocument document, Config config)
        : base(document, config)
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

            var templateContent = new TemplateContent(string.Join(" ", classList.Distinct()), node.InnerHtml);

            node.OuterHtml = this.Template("table", templateContent);
        }
    }
}