namespace BootstrapEmail.Net.Converters;

/// <summary>
/// Class Body.
/// </summary>
public class Body : Base
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Body"/> class.
    /// </summary>
    /// <param name="document">The document.</param>
    /// <param name="config">The configuration.</param>
    public Body(IHtmlDocument document, Config config, IBrowsingContext context)
        : base(document, config, context)
    {
    }

	/// <summary>
	/// Builds this instance.
	/// </summary>
	public virtual void Build()
    {
        var body = this.Document.QuerySelector("body");

        if (body == null)
        {
            return;
        }

        var templateContent = new TemplateContent(
            string.IsNullOrEmpty(body.ClassName) ? "body" : $"{body.ClassName} body",
            body.InnerHtml.Replace("\n", string.Empty));

        body.InnerHtml = $"\r\n    {this.Template("body", templateContent)}";
    }
}