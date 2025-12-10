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
        Color.UseHex = true;

        var style = new StyleDeclaration(parser);
        style.Update(inlineStyle);
        return style;
    }
}