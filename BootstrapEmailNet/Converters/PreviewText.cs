namespace BootstrapEmail.Net.Converters;

public class PreviewText : Base
{
    public PreviewText(IHtmlDocument document)
        : base(document)
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

        Dictionary<string, object> templateContent = new()
                                                         {
                                                             { "classes", "preview" },
                                                             { "contents", previewNode.InnerHtml }
                                                         };

        var previewElement = parser.ParseDocument(this.Template("div", templateContent)).QuerySelector("div");

        if (previewElement != null)
        {
            previewNode.ReplaceWith(previewElement);
        }
    }
}