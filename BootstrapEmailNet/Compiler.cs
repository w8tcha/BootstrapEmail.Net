using System.Reflection;

namespace BootstrapEmail.Net;

using System.IO;

using global::BootstrapEmail.Net.Converters;

using PreMailer.Net;

/// <summary>
/// Class Compiler.
/// </summary>
public class Compiler
{
    /// <summary>
    /// Gets or sets the type.
    /// </summary>
    /// <value>The type.</value>
    public InputType Type { get; set; }

    /// <summary>
    /// Gets or sets the configuration.
    /// </summary>
    /// <value>The configuration.</value>
    public Config Config { get; set; }

    public IBrowsingContext Context { get; set; }

    /// <summary>
    /// Gets or sets the document.
    /// </summary>
    /// <value>The document.</value>
    public IHtmlDocument Document { get; set; }

    /// <summary>
    /// Gets or sets the pre mailer.
    /// </summary>
    /// <value>The pre mailer.</value>
    public PreMailer PreMailer { get; set; }

    /// <summary>
    /// Gets or sets the input HTML.
    /// </summary>
    /// <value>The input HTML.</value>
    public string InputHtml { get; set; }

    /// <summary>
    /// The lock
    /// </summary>
    private static readonly ReaderWriterLockSlim Lock = new();

	/// <summary>
	/// Initializes a new instance of the <see cref="Compiler"/> class.
	/// </summary>
	/// <param name="input">The input.</param>
	/// <param name="config">The configuration.</param>
	/// <param name="type">The type.</param>
	public Compiler(string input, ConfigStore config, InputType type = InputType.String)
    {
        this.Config = new Config(config);

        var configuration = Configuration.Default.WithCss();

        this.Context = BrowsingContext.New(configuration);

        this.Type = type;

	    this.InputHtml = this.Type switch
	    {
		    InputType.String => input,
		    InputType.File => File.ReadAllText(input),
		    _ => input
	    };

	    if (!Directory.Exists(Path.Combine(AppContext.BaseDirectory, this.Config.SassLocation())))
	    {
			GetEmbeddedCoreFiles();
	    }

		var html = this.AddLayout(this.InputHtml);

	    html = EnsureDoctype.Replace(html);

	    this.PreMailer = new PreMailer(html);

	    this.Document = this.PreMailer.Document;
    }

    /// <summary>
    /// Performs the multipart compile.
    /// </summary>
    /// <returns>System.Object.</returns>
    public object PerformMultipartCompile()
    {
        return new { text = this.PerformTextCompile(), html = this.PerformHtmlCompile() };
    }

    /// <summary>
    /// Performs the text compile.
    /// </summary>
    /// <returns>System.String.</returns>
    public string PerformTextCompile()
    {
        return this.PlainText();
    }

    /// <summary>
    /// Performs the HTML compile.
    /// </summary>
    /// <returns>System.String.</returns>
    public string PerformHtmlCompile()
    {
		this.CompileHtml();
        this.InlineCss();
        this.ConfigureHtml();
        return this.FinalizeDocument();
    }

    /// <summary>
    /// Performs the full compile.
    /// </summary>
    /// <returns>System.String.</returns>
    public string PerformFullCompile()
    {
        return this.PerformHtmlCompile();
    }

    /// <summary>
    /// Adds the layout.
    /// </summary>
    /// <param name="html">The HTML.</param>
    /// <returns>System.String.</returns>
    private string AddLayout(string html)
    {
        var config = Configuration.Default;
        using var context = BrowsingContext.New(config);
        using var doc = context.OpenAsync(req => req.Content(html)).Result;

        if (doc.Head is not null && !string.IsNullOrEmpty(doc.Head.InnerHtml))
        {
            return html;
        }

        return Erb.Template(Path.Combine(AppContext.BaseDirectory, this.Config.SassLocation(), this.Config.ConfigStore.layout_file), html);
    }

    /// <summary>
    /// Compiles the HTML.
    /// </summary>
    public void CompileHtml()
    {
        new Body(this.Document, this.Config, this.Context).Build();

        new Block(this.Document, this.Config, this.Context).Build();
        new Button(this.Document, this.Config, this.Context).Build();

        new Badge(this.Document, this.Config, this.Context).Build();
        new Alert(this.Document, this.Config, this.Context).Build();
        new Card(this.Document, this.Config, this.Context).Build();
        new Hr(this.Document, this.Config, this.Context).Build();
        new Container(this.Document, this.Config, this.Context).Build();
        new Grid(this.Document, this.Config, this.Context).Build();
        new Stack(this.Document, this.Config, this.Context).Build();
        new Color(this.Document, this.Config, this.Context).Build();
        new Spacing(this.Document, this.Config, this.Context).Build();
        new Margin(this.Document, this.Config, this.Context).Build();
        new AlignDiv(this.Document, this.Config, this.Context).Build();
        new Spacer(this.Document, this.Config, this.Context).Build();

        new Padding(this.Document, this.Config, this.Context).Build();
        new PreviewText(this.Document, this.Config, this.Context).Build();
        new Paragraph(this.Document, this.Config, this.Context).Build();
    }

    /// <summary>
    /// Converts the Html to text.
    /// </summary>
    /// <returns>System.String.</returns>
    public string PlainText()
    {
        return HtmlUtilities.ConvertToPlainText(this.InputHtml);
    }

    /// <summary>
    /// Moves the Css inline.
    /// </summary>
    public void InlineCss()
    {
        var cssString = SassCache.Compile(SassTypes.SassEmail, this.Config, style: StyleType.Expanded);

        var result = this.PreMailer.MoveCssInline(css: $"{cssString}{GetExistingInlineCss()}");

        // Log Warnings
        if (!this.Config.ConfigStore.sass_log_enabled)
        {
	        return;
        }

        foreach (var warning in result.Warnings)
        {
	        Console.WriteLine(warning);
        }
    }

    private string? GetExistingInlineCss()
    {
	    var head = this.Document.Head;

	    return head?.QuerySelector("style")?.TextContent;
    }

    /// <summary>
    /// Configures the HTML.
    /// </summary>
    public void ConfigureHtml()
    {
		HeadStyle.Build(this.Document, this.Config);
        new AddMissingMetaTags(this.Document, this.Config, this.Context).Build();
        new VersionComment(this.Document, this.Config, this.Context).Build();

        new Table(this.Document, this.Config, this.Context).Build();
        new Td(this.Document, this.Config, this.Context).Build();

        new Align(this.Document, this.Config, this.Context).Build();
    }

    /// <summary>
    /// Finalizes the document.
    /// </summary>
    /// <returns>System.String.</returns>
    public string FinalizeDocument()
    {
        var html = this.Config.ConfigStore.CompileOnlyBody
                       ? this.Document.Body!.InnerHtml
                       : this.Document.ToHtml(new BootstrapEmailFormatter());

        html = ForceEncoding.Replace(html);

        return html;
    }

	/// <summary>
	/// Gets the embedded core files.
	/// </summary>
	private static void GetEmbeddedCoreFiles()
    {
	    var assembly = typeof(BootstrapEmail).GetTypeInfo().Assembly;

	    foreach (var s in assembly.GetManifestResourceNames())
	    {
		    var fileName = s.Replace("BootstrapEmail.Net.", "").Replace(".", Path.DirectorySeparatorChar.ToString());

		    var lastIndex = fileName.LastIndexOf(Path.DirectorySeparatorChar);

		    fileName = string.Concat(fileName.AsSpan(0, lastIndex), ".", fileName.AsSpan(lastIndex + 1));

		    fileName = fileName.Replace($"layout{Path.DirectorySeparatorChar}html.erb", "layout.html.erb");

		    WriteResourceToFile(assembly, s,
			    fileName);
	    }
    }

	/// <summary>
	/// Writes the embedded resource to file.
	/// </summary>
	/// <param name="assembly">The assembly.</param>
	/// <param name="resourceName">Name of the resource.</param>
	/// <param name="fileName">Name of the file.</param>
	private static void WriteResourceToFile(Assembly assembly, string resourceName, string fileName)
    {
	    using var resource = assembly.GetManifestResourceStream(resourceName);

        var dirName = Path.GetDirectoryName(fileName);

        if (!string.IsNullOrEmpty(dirName))
        {
	        Directory.CreateDirectory(dirName);
		}

	    Lock.EnterWriteLock();

	    try
	    {
		    using var fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            resource?.CopyTo(fs);
		}
	    finally
	    {
		    Lock.ExitWriteLock();
	    }
    }
}