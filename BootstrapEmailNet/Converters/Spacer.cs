namespace BootstrapEmail.Net.Converters;

public class Spacer : Base
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Spacer"/> class.
    /// </summary>
    /// <param name="document">The document.</param>
    /// <param name="config">The configuration.</param>
    public Spacer(IHtmlDocument document, Config config)
        : base(document, config)
    {
    }

    private static readonly Regex SpacerClassRegex =
        new(@"^s(-lg)?-\d+$", RegexOptions.None, TimeSpan.FromMilliseconds(100));

    public virtual void Build()
    {
        foreach (var node in this.EachNode("*[class*=s-]"))
        {
            var classes = node.ClassName?.Split(' ') ?? [];

            if (!classes.Any(c => SpacerClassRegex.IsMatch(c)))
            {
                continue;
            }

            var templateContent = new TemplateContent($"{node.ClassName} w-full", "&nbsp;");

            node.OuterHtml = this.Template("table", templateContent);
        }
    }
}