namespace BootstrapEmail.Net.Converters;

using System.Text;

public class Margin : Base
{
    public Margin(IHtmlDocument document)
        : base(document)
    {
    }

    public virtual void Build()
    {
        foreach (var node in this.EachNode("*[class^='my-'], *[class^='mt-'], *[class^='mb-'], *[class*=' my-'], *[class*=' mt-'], *[class*=' mb-']"))
        {
            var className = node.ClassName ?? string.Empty;
            var topClass = Regex.Match(className, @"m[ty]{1}-(lg-)?(\d+)").Value;
            var bottomClass = Regex.Match(className, @"m[by]{1}-(lg-)?(\d+)").Value;

            var nodeCssClass = Regex.Replace(className, @"(m[tby]{1}-(lg-)?\d+)", string.Empty).Trim();

            node.SetAttribute("class", nodeCssClass);

            var html = new StringBuilder();

            if (!string.IsNullOrEmpty(topClass))
            {
                Dictionary<string, object> templateContent = new()
                                                                 {
                                                                     { "classes", $"s-{Regex.Replace(topClass, @"m[ty]{1}-", string.Empty)}" },
                                                                     {
                                                                         "contents",
                                                                         ""
                                                                     }
                                                                 };

                html.Append(this.Template("div", templateContent));
            }

            html.Append(node.OuterHtml);

            if (!string.IsNullOrEmpty(bottomClass))
            {
                Dictionary<string, object> templateContent = new()
                                                                 {
                                                                     { "classes", $"s-{Regex.Replace(bottomClass, @"m[by]{1}-", string.Empty)}" },
                                                                     {
                                                                         "contents",
                                                                         ""
                                                                     }
                                                                 };

                html.Append(this.Template("div", templateContent));
            }

            node.OuterHtml = html.ToString();
        }
    }
}