namespace BootstrapEmail.Net.Converters;

public class Paragraph : Base
{
    public Paragraph(IHtmlDocument document, Config config)
        : base(document, config)
    {
    }

    public virtual void Build()
    {
        foreach (var node in this.EachNode("p").Where(node => !this.IsMargin(node)))
        {
            //this.AddClass(node, "mb-4");
        }
    }
}