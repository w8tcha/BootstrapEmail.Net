namespace BootstrapEmail.Net.Converters;

/// <summary>
/// Class HeadStyle.
/// </summary>
public static partial class HeadStyle
{
	/// <summary>
	/// Builds the specified document.
	/// </summary>
	/// <param name="doc">The document.</param>
	/// <param name="config">The configuration.</param>
	public static void Build(IHtmlDocument doc, Config config)
    {
        var styleNode = BootstrapEmailHead(doc, config);

        if (doc.Head == null)
        {
	        return;
        }

        var headStyle = doc.Head.QuerySelector("style");

        if (headStyle != null)
        {
	        headStyle.TextContent += styleNode.TextContent;
        }
        else
        {
	        doc.Head.AppendChild(styleNode);
	        doc.Head.InsertBefore(doc.CreateTextNode("  "), styleNode);
	        styleNode.InsertAfter(doc.CreateTextNode("\r\n  "));
        }
    }

	private static IHtmlStyleElement BootstrapEmailHead(IDocument doc, Config config)
    {
        var style = doc.CreateElement<IHtmlStyleElement>();

        style.Type = "text/css";
		style.TextContent = PurgedCssFromHead(doc, config);

        return style;
    }

    private static string PurgedCssFromHead(IParentNode doc, Config config)
    {
        var css = SassCache.Compile(SassTypes.Head, config);

        var cssParts = css.Split("/*! allow_purge_after */");
        var defaultCss = cssParts[0];
        var customCss = cssParts.Length > 1 ? cssParts[1] : string.Empty;

        var groups = RegexCustomCss().Matches(customCss);

        customCss = (from @group in groups
                     let selectors = RegexSelectors().Matches(@group.Value).Select(m => m.Groups[1].Value).Distinct()
                     let exist = selectors.Any(selector => doc.QuerySelector(selector) != null)
                     where !exist
                     select @group)
            .Aggregate(customCss, (current, group) => current.Replace(group.Value, string.Empty));

        return $"\r\n      {defaultCss}{customCss}    ";
    }

    [GeneratedRegex(@"\w*\.[\w\-]*[\s\S\n]+?(?=})}{1}", RegexOptions.None)]
    private static partial Regex RegexCustomCss();

    [GeneratedRegex(@"(\.[\w\-]*).*?((,+?)|{+?)", RegexOptions.None)]
    private static partial Regex RegexSelectors();
}