namespace BootstrapEmail.Net.Converters;

/// <summary>
/// Class Paragraph.
/// Implements the <see cref="BootstrapEmail.Net.Converters.Base" />
/// </summary>
/// <seealso cref="BootstrapEmail.Net.Converters.Base" />
public class Paragraph : Base
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Paragraph"/> class.
    /// </summary>
    /// <param name="document">The document.</param>
    /// <param name="config">The configuration.</param>
    public Paragraph(IHtmlDocument document, Config config)
        : base(document, config)
    {
    }

    /// <summary>
    /// Builds this instance.
    /// </summary>
    public virtual void Build()
    {
        /*foreach (var node in this.EachNode("p").Where(node => !IsMargin(node)))
        {
            AddClass(node, "mb-4");
        }*/
    }
}