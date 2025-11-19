namespace BootstrapEmail.Net.Converters;

using System.Text;

public class Stack : Base
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Stack"/> class.
    /// </summary>
    /// <param name="document">The document.</param>
    /// <param name="config">The configuration.</param>
    /// <param name="context">the browsing context.</param>
    public Stack(IHtmlDocument document, Config config, IBrowsingContext context)
        : base(document, config, context)
    {
    }

    public virtual void Build()
    {
        this.StackRow();
        this.StackCol();
    }

    private void StackRow()
    {
        foreach (var node in this.EachNode(".stack-row"))
        {
            var html = new StringBuilder();

            foreach (var templateContent in node.Children.Select(
                         child => new TemplateContent("stack-cell", child.OuterHtml)))
            {
                html.Append(this.Template("td", templateContent));
            }

            var content = new TemplateContent(node.ClassName ?? string.Empty, html.ToString());

            node.OuterHtml = this.Template("table-to-tr", content);
        }
    }

    private void StackCol()
    {
        foreach (var node in this.EachNode(".stack-col"))
        {
            var html = new StringBuilder();

            foreach (var templateContent in node.Children.Select(
                         child => new TemplateContent("stack-cell", child.OuterHtml)))
            {
                html.Append(this.Template("tr", templateContent));
            }

            var content = new TemplateContent(node.ClassName ?? string.Empty, html.ToString());

            node.OuterHtml = this.Template("table-to-tbody", content);
        }
    }
}