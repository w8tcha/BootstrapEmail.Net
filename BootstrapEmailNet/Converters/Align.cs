using BootstrapEmail.Net.Extensions;

using ExCSS;

namespace BootstrapEmail.Net.Converters;

public class Align : Base
{
    /// <summary>
    /// Gets or sets the style sheet parser.
    /// </summary>
    /// <value>
    /// The style sheet parser.
    /// </value>
    public StylesheetParser StyleSheetParser { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Align"/> class.
    /// </summary>
    /// <param name="document">The document.</param>
    /// <param name="config">The configuration.</param>
    /// <param name="styleSheetParser"></param>
    public Align(IHtmlDocument document, Config config, StylesheetParser styleSheetParser)
        : base(document, config)
    {
        this.StyleSheetParser = styleSheetParser;
    }

    public virtual void Build()
    {
        string[] types = ["start", "center", "end"];
        foreach (var type in types)
        {
            var fullType = $"text-{type}";

            foreach (var node in this.EachNode($".{fullType}:not(table):not(td)"))
            {
                this.AlignHelper(node, fullType, type);
            }
        }
    }

    public void AlignHelper(IElement node, string fullType, string type)
    {
        if (node.LocalName == "div")
        {
            return;
        }

        node.ClassList.Remove(fullType);

        if (node.ClassList.Count == 0)
        {
            node.RemoveAttribute("class");
        }

        var styleAttribute = node.GetAttribute("style");

        if (styleAttribute is null)
        {
            return;
        }

        var stylesheet = this.StyleSheetParser.ParseInlineStyle(styleAttribute);

        var align = stylesheet.TextAlign;

        if (!string.IsNullOrEmpty(align))
        {
            stylesheet.TextAlign = string.Empty;

            node.SetAttribute("style", stylesheet.CssText);
        }

        type = type switch
        {
            "start" => "left",
            "end" => "right",
            _ => type
        };

        node.SetAttribute("align", type);
    }
}