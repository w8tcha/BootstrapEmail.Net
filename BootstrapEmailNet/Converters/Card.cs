﻿namespace BootstrapEmail.Net.Converters;

public class Card : Base
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Card"/> class.
    /// </summary>
    /// <param name="document">The document.</param>
    /// <param name="config">The configuration.</param>
    public Card(IHtmlDocument document, Config config)
        : base(document, config)
    {
    }

    public virtual void Build()
    {
        foreach (var node in this.EachNode(".card"))
        {
            var templateContent = new TemplateContent(node.ClassName ?? string.Empty, node.InnerHtml);

            node.OuterHtml = this.Template("table", templateContent);
        }

        foreach (var node in this.EachNode(".card-body"))
        {
            var templateContent = new TemplateContent(node.ClassName ?? string.Empty, node.InnerHtml);

            node.OuterHtml = this.Template("table", templateContent);
        }
    }
}