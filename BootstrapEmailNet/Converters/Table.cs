namespace BootstrapEmail.Net.Converters;

/// <summary>
/// Class Table.
/// </summary>
public class Table : Base
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Table"/> class.
    /// </summary>
    /// <param name="document">The document.</param>
    /// <param name="config">The configuration.</param>
    public Table(IHtmlDocument document, Config config, IBrowsingContext context)
        : base(document, config, context)
    {
    }

	/// <summary>
	/// Builds this instance.
	/// </summary>
	public virtual void Build()
    {
        foreach (var node in this.EachNode("table[bgcolor='']"))
        {
            node.RemoveAttribute("bgcolor");
        }
    }
}