using AngleSharp.Css.Dom;
using AngleSharp.Css.Parser;
using AngleSharp.Css.Values;

namespace BootstrapEmail.Net.Converters;

/// <summary>
/// Class Td.
/// Implements the <see cref="Base" />
/// </summary>
/// <seealso cref="Base" />
public class Td : Base
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Td"/> class.
    /// </summary>
    /// <param name="document">The document.</param>
    /// <param name="config">The configuration.</param>
    /// <param name="context"></param>
    public Td(IHtmlDocument document, Config config, IBrowsingContext context)
        : base(document, config, context)
    {
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

            var parser = this.Context.GetService<ICssParser>();

            if (parser == null)
            {
                return;
            }

            ICssStyleDeclaration style;

            try
            {
                style = parser.ParseDeclaration(styleAttribute);
            }
            catch (Exception)
            {
                return;
            }

            var align = style.GetProperty("text-align");

            if (align is not null && align.Value is not "")
            {
                align.IsImportant = false;

                node.SetAttribute("align", align.Value);

                style.RemoveProperty("text-align");

                node.SetAttribute("style", style.ToCss(new BootstrapEmailStyleFormatter()));
            }

            var backgroundColor = style.GetProperty("background-color");

            if (backgroundColor?.Value is "")
            {
                continue;
            }

            CssColorValue.UseHex = true;

            node.SetAttribute("bgcolor", backgroundColor.Value.ToLowerInvariant());

            style.RemoveProperty("background-color");

            node.SetAttribute("style", style.ToCss(new BootstrapEmailStyleFormatter()));
        }
    }
}