namespace BootstrapEmail.Net.Converters;

public class AddMissingMetaTags : Base
{
    private sealed record Tag(string Query, string Code);

    private static readonly List<Tag> MetaTags = new()
                                                     {
                                                         new Tag(
                                                             "meta[http-equiv=\"Content-Type\"]",
                                                             "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">"),
                                                         new Tag(
                                                             "meta[http-equiv=\"x-ua-compatible\"]",
                                                             "<meta http-equiv=\"x-ua-compatible\" content=\"ie=edge\">"),
                                                         new Tag(
                                                             "meta[name=\"x-apple-disable-message-reformatting\"]",
                                                             "<meta name=\"x-apple-disable-message-reformatting\">"),
                                                         new Tag(
                                                             "meta[name=\"viewport\"]",
                                                             "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">"),
                                                         new Tag(
                                                             "meta[name=\"format-detection\"]",
                                                             "<meta name=\"format-detection\" content=\"telephone=no, date=no, address=no, email=no\">")
                                                     };

    /// <summary>
    /// Initializes a new instance of the <see cref="AddMissingMetaTags"/> class.
    /// </summary>
    /// <param name="document">The document.</param>
    /// <param name="config">The configuration.</param>
    public AddMissingMetaTags(IHtmlDocument document, Config config)
        : base(document, config)
    {
    }

    public virtual void Build()
    {
        var headNode = this.Document.QuerySelector("head");

        var parser = new HtmlParser();

        foreach (var table in from tag in MetaTags
                              where this.Document.QuerySelector(tag.Query) == null
                              select parser.ParseDocument(tag.Code).QuerySelector(tag.Query))
        {
            headNode?.Prepend(table);
        }
    }
}