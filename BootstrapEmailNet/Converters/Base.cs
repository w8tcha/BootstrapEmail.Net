namespace BootstrapEmail.Net.Converters;

/// <summary>
/// Base Converter Class
/// </summary>
public abstract class Base
{
    /// <summary>
    /// Gets or sets the document.
    /// </summary>
    /// <value>The document.</value>
    public IHtmlDocument Document { get; set; }

    /// <summary>
    /// Gets or sets the configuration.
    /// </summary>
    /// <value>The configuration.</value>
    public Config Config { get; set; }

    /// <summary>
    /// The cached templates
    /// </summary>
    private Dictionary<string, string> cachedTemplates = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="Base"/> class.
    /// </summary>
    /// <param name="document">The document.</param>
    /// <param name="config">The configuration.</param>
    protected Base(IHtmlDocument document, Config config)
    {
        this.Document = document;
        this.Config = config;
    }

    /// <summary>
    /// Create Template
    /// </summary>
    /// <param name="file">The file.</param>
    /// <param name="templateContent">The template Content</param>
    /// <returns>System.String.</returns>
    public string Template(string file, TemplateContent templateContent)
    {
        string stringContent;

        if (this.cachedTemplates.TryGetValue(file, out var template))
        {
            stringContent = template;
        }
        else
        {
            this.cachedTemplates = [];

            var path = Path.GetFullPath(
                Path.Combine(
                    AppContext.BaseDirectory,
                    this.Config.SassLocation(),
                    Constants.CoreTemplatesDir,
                    $"{file}.html"));
            stringContent = File.ReadAllText(path).TrimEnd('\n');

            this.cachedTemplates[file] = stringContent;
        }

        return stringContent.Replace(Constants.Template.Classes, templateContent.Classes).Replace(
            Constants.Template.Contents,
            templateContent.Contents);
    }

    public List<IElement> EachNode(string cssLookup)
    {
        // sort by youngest child and traverse backwards up the tree
        var nodes = this.Document.QuerySelectorAll(cssLookup).OrderByDescending(x => x.Ancestors().Count());

        return [.. nodes];
    }

    public static List<IElement> EachChildNode(IElement node, string cssLookup)
    {
        // sort by youngest child and traverse backwards up the tree
        var nodes = node.QuerySelectorAll(cssLookup).OrderByDescending(x => x.Ancestors().Count());

        return [.. nodes];
    }

    /// <summary>
    /// Adds the class name to the class list.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="className">Name of the class.</param>
    protected static void AddClass(IElement node, string className)
    {
        node.ClassList.Add(className);
    }

    protected static bool IsMargin(IElement node)
    {
        return IsMarginTop(node) || IsMarginBottom(node);
    }

    protected static bool IsMarginTop(IElement node)
    {
        return node.ClassName != null && Regex.IsMatch(
                   node.ClassName,
                   @"m[ty]{1}-(lg-)?\d+",
                   RegexOptions.None,
                   TimeSpan.FromMilliseconds(100));
    }

    protected static bool IsMarginBottom(IElement node)
    {
        return node.ClassName != null && Regex.IsMatch(
                   node.ClassName,
                   @"m[by]{1}-(lg-)?\d+",
                   RegexOptions.None,
                   TimeSpan.FromMilliseconds(100));
    }
}