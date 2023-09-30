namespace BootstrapEmail.Net.Converters;

public class Card : Base
{
    public Card(IHtmlDocument document)
        : base(document)
    {
    }

    public virtual void Build()
    {
        foreach (var node in this.EachNode(".card"))
        {
            Dictionary<string, object> templateContent = new()
                                                             {
                                                                 { "classes", node.ClassName ?? string.Empty },
                                                                 { "contents", node.InnerHtml }
                                                             };

            node.OuterHtml = this.Template("table", templateContent);
        }

        foreach (var node in this.EachNode(".card-body"))
        {
            Dictionary<string, object> templateContent = new()
                                                             {
                                                                 { "classes", node.ClassName ?? string.Empty},
                                                                 { "contents", node.InnerHtml }
                                                             };
            node.OuterHtml = this.Template("table", templateContent);
        }
    }
}