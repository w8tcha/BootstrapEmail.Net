namespace BootstrapEmail.Net.Converters;

public class AddMissingMetaTags : Base
{
    private static readonly List<Dictionary<string, string>> MetaTags = new List<Dictionary<string, string>>
                                                                             {
                                                                                 new()
                                                                                     {
                                                                                         { "query", "meta[http-equiv=\"Content-Type\"]" },
                                                                                         { "code", "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">" }
                                                                                     },
                                                                                 new()
                                                                                     {
                                                                                         { "query", "meta[http-equiv=\"x-ua-compatible\"]" },
                                                                                         { "code", "<meta http-equiv=\"x-ua-compatible\" content=\"ie=edge\">" }
                                                                                     },
                                                                                 new()
                                                                                     {
                                                                                         { "query", "meta[name=\"x-apple-disable-message-reformatting\"]" },
                                                                                         { "code", "<meta name=\"x-apple-disable-message-reformatting\">" }
                                                                                     },
                                                                                 new()
                                                                                     {
                                                                                         { "query", "meta[name=\"viewport\"]" },
                                                                                         { "code", "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">" }
                                                                                     },
                                                                                 new()
                                                                                     {
                                                                                         { "query", "meta[name=\"format-detection\"]" },
                                                                                         { "code", "<meta name=\"format-detection\" content=\"telephone=no, date=no, address=no, email=no\">" }
                                                                                     }
                                                                             }.ToList();

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

        foreach (var table in from tagHash in MetaTags
                              where this.Document.QuerySelector(tagHash["query"]) == null
                              select parser.ParseDocument(tagHash["code"]).QuerySelector(tagHash["query"]))
        {
            headNode?.Prepend(this.Document.CreateTextNode("\n    "), table);
        }
    }
}