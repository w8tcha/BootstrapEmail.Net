namespace BootstrapEmail.Net;

/// <summary>
/// Class ConfigStore.
/// </summary>
public class ConfigStore
{
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
    public string sass_location { get; set; } = string.Empty;

    /// <summary>
    /// array of directories for loading sass imports.
    /// </summary>
    /// <value>array of directories for loading sass imports.</value>
    public string[] sass_load_paths { get; set; } = Array.Empty<string>();

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
}