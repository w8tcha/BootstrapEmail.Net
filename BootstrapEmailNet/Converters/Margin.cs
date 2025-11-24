namespace BootstrapEmail.Net.Converters;

using System.Text;

public class Margin : Base
{
    public Margin(IHtmlDocument document, Config config)
        : base(document, config)
    {
    }

    public virtual void Build()
    {
        foreach (var node in this.EachNode(
                     "*[class^='my-'], *[class^='mt-'], *[class^='mb-'], *[class*=' my-'], *[class*=' mt-'], *[class*=' mb-']"))
        {
            var topClass = string.Empty;
            var bottomClass = string.Empty;

            if (!string.IsNullOrEmpty(node.ClassName))
            {
                var className = node.ClassName;

                topClass = Regex.Match(
                    className,
                    @"m[ty]{1}-(lg-)?(\d+)",
                    RegexOptions.None,
                    TimeSpan.FromMilliseconds(100)).Value;
                bottomClass = Regex.Match(
                    className,
                    @"m[by]{1}-(lg-)?(\d+)",
                    RegexOptions.None,
                    TimeSpan.FromMilliseconds(100)).Value;

                var nodeCssClass = Regex.Replace(
                    className,
                    @"(m[tby]{1}-(lg-)?\d+)",
                    string.Empty,
                    RegexOptions.None,
                    TimeSpan.FromMilliseconds(100)).Trim();

                node.SetAttribute("class", nodeCssClass);
            }

            var html = new StringBuilder();

            if (!string.IsNullOrEmpty(topClass))
            {
                var templateContent = new TemplateContent(
                    $"s-{Regex.Replace(topClass, "m[ty]{1}-", string.Empty, RegexOptions.None, TimeSpan.FromMilliseconds(100))}",
                    string.Empty);

                html.Append(this.Template("div", templateContent));
            }

            html.Append(node.OuterHtml);

            if (!string.IsNullOrEmpty(bottomClass))
            {
                var templateContent = new TemplateContent(
                    $"s-{Regex.Replace(bottomClass, "m[by]{1}-", string.Empty, RegexOptions.None, TimeSpan.FromMilliseconds(100))}",
                    string.Empty);

                html.Append(this.Template("div", templateContent));
            }

            node.OuterHtml = html.ToString();
        }
    }
}