using BootstrapEmail.Net.Extensions;
using ExCSS;

namespace BootstrapEmail.Net.Converters;

/// <summary>
/// Class Td.
/// Implements the <see cref="Base" />
/// </summary>
/// <seealso cref="Base" />
public class Td : Base
{
    /// <summary>
    /// Gets or sets the style sheet parser.
    /// </summary>
    /// <value>
    /// The style sheet parser.
    /// </value>
    public StylesheetParser StyleSheetParser { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Td"/> class.
    /// </summary>
    /// <param name="document">The document.</param>
    /// <param name="config">The configuration.</param>
    /// <param name="styleSheetParser"></param>
    public Td(IHtmlDocument document, Config config, StylesheetParser styleSheetParser)
        : base(document, config)
    {
        this.StyleSheetParser = styleSheetParser;
    }

    /// <summary>
    /// Builds this instance.
    /// </summary>
    public virtual void Build()
    {
        foreach (var node in this.EachNode("td"))
        {
            var styleAttribute = node.GetAttribute(AttributeNames.Style);

            if (styleAttribute is null)
            {
                return;
            }

            var stylesheet = this.StyleSheetParser.ParseInlineStyle(styleAttribute);

            var align = stylesheet.TextAlign;

            if (!string.IsNullOrEmpty(align))
            {
                node.SetAttribute("align", align);

                stylesheet.TextAlign = string.Empty;

                node.SetAttribute("style", stylesheet.CssText);
            }

            var backgroundColor = stylesheet.BackgroundColor;

            if (backgroundColor is "")
            {
                continue;
            }

            node.SetAttribute("bgcolor", backgroundColor);

            stylesheet.BackgroundColor = string.Empty;

            node.SetAttribute("style", stylesheet.CssText);
        }
    }
}