using AngleSharp.Css.Parser;

namespace BootstrapEmail.Net.Converters;

public class Align : Base
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Align"/> class.
    /// </summary>
    /// <param name="document">The document.</param>
    /// <param name="config">The configuration.</param>
    /// <param name="context">the browsing context.</param>
    public Align(IHtmlDocument document, Config config, IBrowsingContext context)
        : base(document, config, context)
    {
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

        var parser = this.Context.GetService<ICssParser>();

        if (parser == null)
        {
            return;
        }

        var style = parser.ParseDeclaration(styleAttribute);

        style.Update(styleAttribute);

        var align = style.GetProperty("text-align");

        if (align is not null && align.Value is not "")
        {
            style.RemoveProperty("text-align");

            node.SetAttribute("style", style.ToCss(new BootstrapEmailStyleFormatter()));
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