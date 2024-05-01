namespace BootstrapEmail.Net;

/// <summary>
/// Class ConfigStore.
/// </summary>
public class ConfigStore
{
#pragma warning disable IDE1006 // Naming Styles
    /// <summary>
    /// path to main sass file.
    /// </summary>
    /// <value>path to main sass file.</value>
    public string sass_email_location { get; set; } = string.Empty;

    /// <summary>
    /// main sass file passed in as a string.
    /// </summary>
    /// <value>main sass file passed in as a string.</value>
    public string sass_email_string { get; set; } = "bootstrap-email.scss";

    /// <summary>
    /// path to head sass file.
    /// </summary>
    /// <value>path to head sass file.</value>
    public string sass_head_location { get; set; } = string.Empty;

    /// <summary>
    /// head sass file passed in as a string.
    /// </summary>
    /// <value>head sass file passed in as a string.</value>
    public string sass_head_string { get; set; } = "bootstrap-head.scss";

    /// <summary>
    /// Gets or sets the sass location.
    /// </summary>
    /// <value>The sass location.</value>
    public string sass_location { get; set; } = "core/";

    /// <summary>
    /// path to tmp folder for sass cache
    /// </summary>
    /// <value>path to tmp folder for sass cache.</value>
    public string sass_cache_location { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the layout file.
    /// </summary>
    /// <value>The layout file.</value>
    public string layout_file { get; set; } = "layout.html.erb";

    /// <summary>
    /// turn on or off sass log when caching new sass
    /// </summary>
    /// <value>turn on or off sass log when caching new sass</value>
    public bool sass_log_enabled { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether Return the plain text version of the email.
    /// </summary>
    public bool plain_text { get; set; }
#pragma warning restore IDE1006 // Naming Styles

    /// <summary>
    /// Gets or sets a value indicating whether [compile only body].
    /// </summary>
    /// <value><c>true</c> if [compile only body]; otherwise, <c>false</c>.</value>
    public bool CompileOnlyBody { get; set; }

    /// <summary>
    /// Gets or sets the type of the dart sass native.
    /// </summary>
    /// <value>The type of the dart sass native.</value>
    public DartSassNativeType? DartSassNativeType { get; set; } = null;

	/// <summary>
	/// Gets or sets the CSS email path. If set it skips the sass parsing.
	/// </summary>
	/// <value>The CSS email path.</value>
	public string CssEmailPath { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the CSS head path. If set it skips the sass parsing.
	/// </summary>
	/// <value>The CSS head path.</value>
	public string CssHeadPath { get; set; } = string.Empty;
}