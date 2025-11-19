namespace BootstrapEmail.Net;

using AngleSharp.Html;
using AngleSharp.Text;

/// <summary>
/// Class BootstrapEmailFormatter.
/// Implements the <see cref="HtmlMarkupFormatter" />
/// </summary>
/// <seealso cref="HtmlMarkupFormatter" />
internal class BootstrapEmailFormatter : HtmlMarkupFormatter
{
    /// <summary>
    /// The indent string
    /// </summary>
    private readonly string _indentString;

    /// <summary>
    /// The indent count
    /// </summary>
    private int _indentCount;

    /// <summary>
    /// The preserve text formatting
    /// </summary>
    private readonly IEnumerable<INode>? _preserveTextFormatting;

    /// <summary>
    /// Initializes a new instance of the <see cref="BootstrapEmailFormatter"/> class.
    /// </summary>
    public BootstrapEmailFormatter()
    {
        this._indentCount = 0;
        this._indentString = "\t";
        this.NewLine = "\n";
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BootstrapEmailFormatter"/> class.
    /// </summary>
    /// <param name="preserveTextFormatting">The preserve text formatting.</param>
    public BootstrapEmailFormatter(IEnumerable<INode> preserveTextFormatting)
    {
        this._indentCount = 0;
        this._indentString = "\t";
        this.NewLine = "\n";
        this._preserveTextFormatting = preserveTextFormatting.SelectMany(x => x.ChildNodes).Where(y => y is ICharacterData);
    }

    public override string Doctype(IDocumentType doctype)
    {
        return
            """<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">"""  + this.NewLine;
    }

    public override string Text(ICharacterData text)
    {
        if (this._preserveTextFormatting?.Contains(text) == true)
        {
            return text.Data.TrimEnd('\n').Replace("\n", this.IndentBefore(1));
        }

        var content = text.Data;
        var before = string.Empty;
        var singleLine = content.Replace(Symbols.LineFeed, Symbols.Space);

        if (text.NextSibling is not ICharacterData)
        {
            singleLine = singleLine.TrimEnd();
        }

        if (singleLine.Length <= 0 || text.PreviousSibling is ICharacterData || !singleLine[0].IsSpaceCharacter())
        {
            return before + EscapeText(singleLine);
        }

        singleLine = singleLine.TrimStart();
        before = this.IndentBefore();

        return before + EscapeText(singleLine);
    }

    public override string OpenTag(IElement element, bool selfClosing)
    {
        var before = string.Empty;
        var previousSibling = element.PreviousSibling as IText;

        if (element.ParentElement != null && (previousSibling is null || EndsWithSpace(previousSibling)))
        {
            before = this.IndentBefore();
        }

        this._indentCount++;
        return before + base.OpenTag(element, selfClosing);
    }

    public override string CloseTag(IElement element, bool selfClosing)
    {
        this._indentCount--;
        var before = string.Empty;
        var lastChild = element.LastChild as IText;

        if (element.HasChildNodes && (lastChild is null || EndsWithSpace(lastChild)))
        {
            before = this.IndentBefore();
        }

        return before + base.CloseTag(element, selfClosing);
    }

    /// <summary>
    /// Gets or sets the newline string.
    /// </summary>
    public string NewLine { get; set; }

    private static bool EndsWithSpace(ICharacterData text)
    {
        var content = text.Data;
        return content.Length > 0 && content[^1].IsSpaceCharacter();
    }

    private string IndentBefore(int i = 0) =>
        $"{this.NewLine}{string.Join(string.Empty, Enumerable.Repeat(this._indentString, this._indentCount - i))}";
}