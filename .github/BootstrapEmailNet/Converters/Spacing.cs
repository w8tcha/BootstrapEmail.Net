namespace BootstrapEmail.Net.Converters;

public class Spacing : Base
{
    public Spacing(IHtmlDocument document)
        : base(document)
    {
    }

    public virtual void Build()
    {
        foreach (var node in this.EachNode("*[class*=space-y-]"))
        {
            var spacer = node.ClassName.Split(' ').FirstOrDefault(c => c.StartsWith("space-y-"));
            spacer = spacer[7..];
            var children = node.Children.Where(e => e != node.Children.Last());
            foreach (var child in children)
            {
                if (this.IsMarginBottom(child))
                {
                    continue;
                }

                this.AddClass(child, $"mb-{spacer}");
            }
        }
    }
}