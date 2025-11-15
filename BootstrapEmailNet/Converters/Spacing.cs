namespace BootstrapEmail.Net.Converters;

public class Spacing : Base
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Spacing"/> class.
    /// </summary>
    /// <param name="document">The document.</param>
    /// <param name="config">The configuration.</param>
    public Spacing(IHtmlDocument document, Config config, IBrowsingContext context)
        : base(document, config, context)
    {
    }

    public virtual void Build()
    {
        foreach (var node in this.EachNode("*[class*=space-y-]"))
        {
            var className = node.ClassName ?? string.Empty;
            var spacer = Array.Find(className.Split(' '), c => c.StartsWith("space-y-"));
            spacer = spacer?[7..];
            var children = node.Children.Where(e => e != node.Children[^1]);

            foreach (var child in children.Where(child => !IsMarginBottom(child)))
            {
                AddClass(child, $"mb-{spacer}");
            }
        }
    }
}