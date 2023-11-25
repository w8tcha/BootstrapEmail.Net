namespace BootstrapEmail.Net.Converters;

/// <summary>
/// Class VersionComment.
/// Implements the <see cref="BootstrapEmail.Net.Converters.Base" />
/// </summary>
/// <seealso cref="BootstrapEmail.Net.Converters.Base" />
public class VersionComment : Base
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VersionComment"/> class.
    /// </summary>
    /// <param name="document">The document.</param>
    /// <param name="config">The configuration.</param>
    public VersionComment(IHtmlDocument document, Config config)
        : base(document, config)
    {
    }

    private INode BootstrapEmailComment()
    {
        var version = this.GetType().Assembly.GetName().Version;

        return this.Document.CreateComment(
            version != null
                ? $" Compiled with Bootstrap Email version: {version.ToString(3)} "
                : " Compiled with Bootstrap Email");
    }

    public virtual void Build()
    {
        var head = this.Document.Head;

        var commentNode = this.BootstrapEmailComment();

        if (head != null)
        {
            head.Prepend(commentNode);

            head.InsertBefore(this.Document.CreateTextNode("\r\n    "), commentNode);
        }

        commentNode.InsertAfter(this.Document.CreateTextNode("\r"));
    }
}