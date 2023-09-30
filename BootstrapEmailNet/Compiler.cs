namespace BootstrapEmail.Net;

using global::BootstrapEmail.Net.Converters;

using LibSass.Compiler;
using LibSass.Compiler.Options;

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
    /// Gets or sets the sass compiler.
    /// </summary>
    /// <value>The sass compiler.</value>
    public SassCompiler SassCompiler { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Compiler"/> class.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="config">The configuration.</param>
    /// <param name="type">The type.</param>
    public Compiler(string input, ConfigStore config, InputType type = InputType.String)
    {
        this.Config = new Config(config);
        this.Type = type;

        var html = this.Type switch
            {
                InputType.String => input,
                InputType.File => File.ReadAllText(input),
                _ => input
            };

        html = AddLayout(html);

        this.SassCompiler = new SassCompiler(new SassOptions { IncludePaths = this.Config.SassLoadPaths() });

        this.PreMailer = new PreMailer(html);

        this.Document = this.PreMailer.Document;
    }

    /*
    public object PerformMultipartCompile()
    {
        return new { text = this.PerformTextCompile(), html = this.PerformHtmlCompile() };
    }

    public string PerformTextCompile()
    {
        return this.PlainText();
    }*/

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
    private static string AddLayout(string html)
    {
        var config = Configuration.Default;
        using var context = BrowsingContext.New(config);
        using var doc = context.OpenAsync(req => req.Content(html)).Result;

        if (doc.Head is not null && !string.IsNullOrEmpty(doc.Head.InnerHtml))
        {
            return html;
        }

        return Erb.Template(Path.Combine(AppContext.BaseDirectory, "core/layout.html.erb"), html);
    }

    /// <summary>
    /// Compiles the HTML.
    /// </summary>
    public void CompileHtml()
    {
        new Body(this.Document).Build();

        new Block(this.Document).Build();
        new Button(this.Document).Build();

        new Badge(this.Document).Build();
        new Alert(this.Document).Build();
        new Card(this.Document).Build();
        new Hr(this.Document).Build();
        new Container(this.Document).Build();
        new Grid(this.Document).Build();
        new Stack(this.Document).Build();
        new Color(this.Document).Build();
        new Spacing(this.Document).Build();
        new Margin(this.Document).Build();

        new Spacer(this.Document).Build();
        new Align(this.Document).Build();

        new Padding(this.Document).Build();
        new PreviewText(this.Document).Build();
        new Table(this.Document).Build();
        new Paragraph(this.Document).Build();
    }

    /// <summary>
    /// Moves the Css inline.
    /// </summary>
    public void InlineCss()
    {
        var cssString = SassCache.Compile(Constants.SassTypes.SassEmail, this.Config, style: SassOutputStyle.Expanded);

        cssString = cssString.Replace("border-width:", "border:");
        cssString = cssString.Replace("Margin", "margin", StringComparison.InvariantCulture);

        this.PreMailer.MoveCssInline(css: cssString );
    }

    /*
    public string PlainText()
    {
        // TODO : Not supported yet in PreMailer.NET
    }*/

    /// <summary>
    /// Configures the HTML.
    /// </summary>
    public void ConfigureHtml()
    {
        HeadStyle.Build(this.Document, this.Config);
        new AddMissingMetaTags(this.Document).Build();
        new VersionComment(this.Document).Build();

        new Td(this.Document).Build();
    }

    /// <summary>
    /// Finalizes the document.
    /// </summary>
    /// <returns>System.String.</returns>
    public string FinalizeDocument()
    {
        var html = this.Document.ToHtml().Replace(">\n", ">\r\n").Replace("}\n", "}\r\n");

        // TODO optimize
        ///html = SupportUrlTokens.Replace(html);
        html = EnsureDoctype.Replace(html);
        html = ForceEncoding.Replace(html);

        return html;
    }
}