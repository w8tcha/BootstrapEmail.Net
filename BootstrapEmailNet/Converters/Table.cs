namespace BootstrapEmail.Net.Converters;

public class Table : Base
{
    public Table(IHtmlDocument document, Config config)
        : base(document, config)
    {
    }

    public virtual void Build()
    {
        foreach (var node in this.EachNode("table"))
        {
            node.SetAttribute("border", "0");
            node.SetAttribute("cellpadding", "0");
            node.SetAttribute("cellspacing", "0");
        }
    }
}