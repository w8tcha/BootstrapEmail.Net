namespace BootstrapEmail.Net.Converters;

public class PreviewText : Base
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PreviewText"/> class.
    /// </summary>
    /// <param name="document">The document.</param>
    /// <param name="config">The configuration.</param>
    public PreviewText(IHtmlDocument document, Config config, IBrowsingContext context)
        : base(document, config, context)
    {
    }

    public virtual void Build()
    {
        var previewNode = this.Document.QuerySelectorAll("preview").FirstOrDefault();

        if (previewNode == null)
        {
            return;
        }

        var parser = new HtmlParser();

        // https://www.litmus.com/blog/the-little-known-preview-text-hack-you-may-want-to-use-in-every-email/
        // apply spacing after the text max of 278 characters so it doesn't show body text
        previewNode.InnerHtml = previewNode.InnerHtml.Truncate(278, "&#847; &zwnj; &nbsp; ");

        var templateContent = new TemplateContent("preview", previewNode.InnerHtml);

        var previewElement = parser.ParseDocument(this.Template("div", templateContent)).QuerySelector("div");

        if (previewElement != null)
        {
            previewNode.ReplaceWith(previewElement);
        }
    }
}