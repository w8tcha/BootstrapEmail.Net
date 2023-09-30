namespace BootstrapEmail.Net;

/// <summary>
/// Class Config.
/// </summary>
public class Config
{
    /// <summary>
    /// The configuration store
    /// </summary>
    private readonly ConfigStore configStore;

    /// <summary>
    /// Initializes a new instance of the <see cref="Config"/> class.
    /// </summary>
    /// <param name="configStore">The configuration store.</param>
    public Config(ConfigStore configStore)
    {
        this.configStore = configStore;
    }

    /// <summary>
    /// Loads the sass file
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>System.String.</returns>
    public string SassStringFor(string type)
    {
        var subType = type.Replace("bootstrap-", string.Empty);
        var fileName = this.ConfigForOption($"sass_{subType}_string");

        var location = this.ConfigForOption($"sass_{subType}_location");

        if (!string.IsNullOrEmpty(fileName))
        {
            var path = Path.Combine(string.IsNullOrEmpty(location) ? AppContext.BaseDirectory : location, fileName);

            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
        }

        string[] lookupLocations = { $"{type}.scss", $"core/{type}.scss" };

        var locations = Array.FindAll(
            lookupLocations,
            path => File.Exists(Path.GetFullPath(path, AppContext.BaseDirectory)));

        return locations.Length > 0
                   ? File.ReadAllText(Path.GetFullPath(locations[0], AppContext.BaseDirectory))
                   : string.Empty;
    }

    /// <summary>
    /// Gets the sass folder location
    /// </summary>
    /// <returns>System.String.</returns>
    public string SassLocation()
    {
        return this.configStore.sass_location;
    }

    /// <summary>
    /// Adds the sass load path
    /// </summary>
    /// <returns>System.String[].</returns>
    public string[] SassLoadPaths()
    {
        string[] pathsArray = { Path.Combine(AppContext.BaseDirectory, Constants.SassDir) };

        var customLoadPaths = this.configStore.sass_load_paths;

        return pathsArray.Concat(customLoadPaths).ToArray();
    }

    /// <summary>
    /// Gets the Sass Cache location.
    /// </summary>
    /// <returns>System.String.</returns>
    public string SassCacheLocation()
    {
        var option = this.configStore.sass_cache_location;

        return !string.IsNullOrEmpty(option)
                   ? option
                   : Path.Combine(
                       Directory.Exists(AppContext.BaseDirectory)
                           ? Directory.GetCurrentDirectory()
                           : Path.GetTempPath(),
                       Constants.SassCache,
                       Constants.SassTypes.SassEmail);
    }

    public bool SassLogEnabled()
    {
        return this.configStore.sass_log_enabled;
    }

    /// <summary>
    /// Gets the config setting by string
    /// </summary>
    /// <param name="option">The option.</param>
    /// <returns>System.Nullable&lt;System.String&gt;.</returns>
    private string? ConfigForOption(string option)
    {
        var property = this.configStore.GetType().GetProperty(option);

        if (property == null)
        {
            return string.Empty;
        }

        var value = property.GetValue(this.configStore);

        return value != null ? value.ToString() : string.Empty;
    }
}