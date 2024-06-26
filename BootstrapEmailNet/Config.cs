﻿namespace BootstrapEmail.Net;

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
    /// <param name="sassType">The sass type.</param>
    /// <returns>System.String.</returns>
    public string SassStringFor(SassTypes sassType)
    {
	    var fileName = sassType is SassTypes.Head ? this.ConfigStore.sass_head_string : this.ConfigStore.sass_email_string;
	    var location = sassType is SassTypes.Head
		    ? this.ConfigStore.sass_head_location
		    : this.ConfigStore.sass_email_location;

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

        var type = sassType is SassTypes.Head
	        ? Constants.SassTypes.Head
	        : Constants.SassTypes.SassEmail;

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

        return [.. pathsArray];
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
	        this.ConfigStore.sass_email_string.Replace(".scss", string.Empty));
    }
}