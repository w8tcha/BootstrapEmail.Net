namespace BootstrapEmail.Net.Converters;

public class Block : Base
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Block"/> class.
    /// </summary>
    /// <param name="document">The document.</param>
    /// <param name="config">The configuration.</param>
    public Block(IHtmlDocument document, Config config, IBrowsingContext context)
        : base(document, config, context)
    {
    }

    public virtual void Build()
    {
        foreach (var node in this.EachNode("block, .to-table"))
        {
            var classList = node.ClassList;

            // add .to-table if it's not already there
            classList.Add("to-table");

            var templateContent = new TemplateContent(string.Join(" ", classList.Distinct()), node.InnerHtml);

            node.OuterHtml = this.Template("table", templateContent);
        }
    }
}