using ExCSS;

namespace BootstrapEmail.Net.Extensions;

/// <summary>
/// Class StylesheetParser Extensions.
/// </summary>
public static class StylesheetParserExtensions
{
    extension(StylesheetParser parser)
    {
        /// <summary>
        /// Parses the inline style.
        /// </summary>
        /// <param name="inlineStyle">The inline style.</param>
        /// <returns>Returns the inline styles as StyleDeclaration</returns>
        public StyleDeclaration ParseInlineStyle(string inlineStyle)
        {
            Color.UseHex = true;

            var style = new StyleDeclaration(parser);
            style.Update(inlineStyle);
            return style;
        }
    }
}