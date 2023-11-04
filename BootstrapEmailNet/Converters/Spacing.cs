namespace BootstrapEmail.Net.Converters;

public class Spacing : Base
{
    public Spacing(IHtmlDocument document, Config config)
        : base(document, config)
    {
    }

    public virtual void Build()
    {
        foreach (var node in this.EachNode("*[class*=space-y-]"))
        {
            var className = node.ClassName ?? string.Empty;
            var spacer = Array.Find(className.Split(' '), c => c.StartsWith("space-y-"));
            spacer = spacer?[7..];
            var children = node.Children.Where(e => e != node.Children.Last());

            foreach (var child in children.Where(child => !IsMarginBottom(child)))
            {
                AddClass(child, $"mb-{spacer}");
            }
        }
    }
}