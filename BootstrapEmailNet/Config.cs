namespace BootstrapEmail.Net;

/// <summary>
/// Class Config.
/// </summary>
public class Config
{
    /// <summary>
    /// The configuration store
    /// </summary>
    public readonly ConfigStore ConfigStore;

    /// <summary>
    /// Initializes a new instance of the <see cref="Config"/> class.
    /// </summary>
    /// <param name="configStore">The configuration store.</param>
    public Config(ConfigStore configStore)
    {
        this.ConfigStore = configStore;
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
            var path = string.IsNullOrEmpty(location)
                           ? Path.Combine(AppContext.BaseDirectory, this.SassLocation(), fileName)
                           : Path.Combine(location, fileName);

            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
        }

        string[] lookupLocations = [$"{type}.scss", $"core/{type}.scss"];

        var locations = Array.FindAll(
            lookupLocations,
            path => File.Exists(Path.GetFullPath(path, Path.Combine(AppContext.BaseDirectory, this.SassLocation()))));

        return locations.Length > 0
                   ? File.ReadAllText(Path.GetFullPath(locations[0], Path.Combine(AppContext.BaseDirectory, this.SassLocation())))
                   : string.Empty;
    }

    /// <summary>
    /// Gets the sass folder location
    /// </summary>
    /// <returns>System.String.</returns>
    public string SassLocation()
    {
        return this.ConfigStore.sass_location;
    }

    /// <summary>
    /// Adds the sass load path
    /// </summary>
    /// <returns>System.String[].</returns>
    public string[] SassLoadPaths()
    {
        string[] pathsArray = [Path.Combine(AppContext.BaseDirectory, this.SassLocation())];

        var customLoadPaths = this.ConfigStore.sass_load_paths;

        return pathsArray.Concat(customLoadPaths).ToArray();
    }

    /// <summary>
    /// Gets the Sass Cache location.
    /// </summary>
    /// <returns>System.String.</returns>
    public string SassCacheLocation()
    {
        var option = this.ConfigStore.sass_cache_location;

        if (!string.IsNullOrEmpty(option))
        {
            return option;
        }

        return Path.Combine(
            Directory.Exists(AppContext.BaseDirectory) ? Directory.GetCurrentDirectory() : Path.GetTempPath(),
            Constants.SassCache,
            Constants.SassTypes.SassEmail);
    }

    public bool SassLogEnabled()
    {
        return this.ConfigStore.sass_log_enabled;
    }

    /// <summary>
    /// Gets the config setting by string
    /// </summary>
    /// <param name="option">The option.</param>
    /// <returns>System.Nullable&lt;System.String&gt;.</returns>
    private string? ConfigForOption(string option)
    {
        var property = this.ConfigStore.GetType().GetProperty(option);

        if (property == null)
        {
            return string.Empty;
        }

        var value = property.GetValue(this.ConfigStore);

        return value != null ? value.ToString() : string.Empty;
    }
}