namespace BootstrapEmail.Net.Converters;

public class Container : Base
{
    public Container(IHtmlDocument document)
        : base(document)
    {
    }

    public virtual void Build()
    {
        foreach (var node in this.EachNode(".container"))
        {
            Dictionary<string, object> templateContent = new()
                                                             {
                                                                 { "classes", node.ClassName },
                                                                 { "contents", node.InnerHtml }
                                                             };

            node.OuterHtml = this.Template("container", templateContent);
        }

        foreach (var node in this.EachNode(".container-fluid"))
        {
            Dictionary<string, object> templateContent = new()
                                                             {
                                                                 { "classes", node.ClassName },
                                                                 { "contents", node.InnerHtml }
                                                             };

            node.OuterHtml = this.Template("table", templateContent);
        }
    }
}