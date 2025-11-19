namespace BootstrapEmail.Net.Converters;

/// <summary>
/// Class Paragraph.
/// </summary>
public class Paragraph : Base
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Paragraph"/> class.
    /// </summary>
    /// <param name="document">The document.</param>
    /// <param name="config">The configuration.</param>
    /// <param name="context">the browsing context.</param>
    public Paragraph(IHtmlDocument document, Config config, IBrowsingContext context)
        : base(document, config, context)
    {
    }

    /// <summary>
    /// Builds this instance.
    /// </summary>
    public virtual void Build()
    {
        foreach (var node in this.EachNode("p").Where(node => !IsMargin(node)))
        {
            AddClass(node, "mb-4");
        }
    }
}