using ExCSS;

namespace BootstrapEmail.Net.Extensions;

/// <summary>
/// Class StylesheetParser Extensions.
/// </summary>
public static class StylesheetParserExtensions
{
    /// <summary>
    /// Parses the inline style.
    /// </summary>
    /// <param name="inlineStyle">The inline style.</param>
    /// <returns>Returns the inline styles as StyleDeclaration</returns>
    public static StyleDeclaration ParseInlineStyle(this StylesheetParser parser, string inlineStyle)
    {
        // StyleDeclaration has no public constructor, so borrow the declaration of an empty rule
        var style = parser.Parse("*{}").StyleRules.OfType<StyleRule>().First().Style;
        style.Update(inlineStyle);
        return style;
    }

    /// <summary>
    /// Serializes the declaration for use in a style attribute.
    /// </summary>
    /// <param name="style">The style declaration.</param>
    /// <returns>The compressed css text with hex colors.</returns>
    public static string ToInlineCss(this StyleDeclaration style)
    {
        return style.ToCss(InlineStyleFormatter.Instance);
    }
}
