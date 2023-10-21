namespace BootstrapEmail.Net.Converters;

public class Td : Base
{
    public Td(IHtmlDocument document, Config config)
        : base(document, config)
    {
    }

    public virtual void Build()
    {
        var regexTextAlign = new Regex("text-align:(.*?);", RegexOptions.None, TimeSpan.FromMilliseconds(100));

        var regexBackgroundColor = new Regex("background-color:(.*?);|background-color:(.*?){7}$", RegexOptions.None, TimeSpan.FromMilliseconds(100));

        foreach (var node in this.EachNode("td"))
        {
            var style = node.GetAttribute("style");

            if (style is null)
            {
                continue;
            }

            var match = regexTextAlign.Match(style);

            if (match.Success)
            {
                var replace = match.Groups[0].Value;
                var align = match.Groups[1].Value;

                style = style.Replace(replace, string.Empty);

                if (node.GetAttribute("align") == null)
                {
                    node.SetAttribute("align", align.Trim());
                }

                node.SetAttribute("style", style);
            }

            var matchBackgroundColor = regexBackgroundColor.Match(style);

            if (matchBackgroundColor.Success)
            {
                var replace = matchBackgroundColor.Groups[0].Value;
                var color = !string.IsNullOrEmpty(matchBackgroundColor.Groups[1].Value)
                                ? matchBackgroundColor.Groups[1].Value
                                : matchBackgroundColor.Groups[2].Value;

                node.SetAttribute("bgcolor", color.Trim());
                node.SetAttribute("style", style.Replace(replace, string.Empty));
            }
        }

        foreach (var node in from node in this.EachNode("table")
                             where node.HasAttribute("bgcolor")
                             let color = node.GetAttribute("bgcolor")
                             where color.Equals(string.Empty)
                             select node)
        {
            node.RemoveAttribute("bgcolor");
        }
    }
}