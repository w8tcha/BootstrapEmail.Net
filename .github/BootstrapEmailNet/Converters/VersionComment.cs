namespace BootstrapEmail.Net.Converters;

public class VersionComment : Base
{
    public VersionComment(IHtmlDocument document)
        : base(document)
    {
    }

    private INode BootstrapEmailComment()
    {
        var version = this.GetType().Assembly.GetName().Version.ToString(3);

        return this.Document.CreateComment($" Compiled with Bootstrap Email version: {version} ");
    }

    public virtual void Build()
    {
        var head = this.Document.Head;

        var commentNode = this.BootstrapEmailComment();

        head?.Prepend(commentNode);

        head.InsertBefore(this.Document.CreateTextNode("\r\n    "), commentNode);
        commentNode.InsertAfter(this.Document.CreateTextNode("\r"));
    }
}