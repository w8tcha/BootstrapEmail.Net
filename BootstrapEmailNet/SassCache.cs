using System.Text;

namespace BootstrapEmail.Net;

using System;
using System.Collections.Concurrent;
using System.IO;
using System.Security.Cryptography;

using DartSassHost;

using JavaScriptEngineSwitcher.Jint;

/// <summary>
/// Class SassCache.
/// </summary>
public class SassCache
{
    /// <summary>
    /// The type
    /// </summary>
    private readonly SassTypes _type;

    /// <summary>
    /// The configuration
    /// </summary>
    private readonly Config _config;

    /// <summary>
    /// The style
    /// </summary>
    private readonly StyleType _style;

    /// <summary>
    /// The sass configuration
    /// </summary>
    private readonly string _sassConfig;

    /// <summary>
    /// The checksum
    /// </summary>
    private readonly string _checksum;

    /// <summary>
    /// The cache dir
    /// </summary>
    private readonly string _cacheDir;

	/// <summary>
	/// Compiles the specified type.
	/// </summary>
	/// <param name="type">The sass type.</param>
	/// <param name="config">The configuration.</param>
	/// <param name="style">The style.</param>
	/// <returns>System.String.</returns>
	public static string Compile(SassTypes type, Config config, StyleType style = StyleType.Compressed)
    {
        return new SassCache(type, config, style).Compile();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SassCache"/> class.
    /// </summary>
    /// <param name="type">The sass type.</param>
    /// <param name="config">The configuration.</param>
    /// <param name="style">The style.</param>
    public SassCache(SassTypes type, Config config, StyleType style)
    {
        this._type = type;
        this._config = config;
        this._style = style;
        this._sassConfig = this.LoadSassConfig();
        this._checksum = this.ChecksumFiles();
        this._cacheDir = config.SassCacheLocation();
	}

	/// <summary>
	/// Compile
	/// </summary>
	/// <returns>System.String.</returns>
    public string Compile()
    {
        // Load Css File and skip Sass Parsing?!
        switch (this._type)
        {
	        case SassTypes.SassEmail when !string.IsNullOrEmpty(this._config.ConfigStore.CssEmailPath):
	        {
		        var filePath = Path.Combine(Directory.GetCurrentDirectory(), this._config.ConfigStore.CssEmailPath);

		        if (File.Exists(filePath))
		        {
			        return ReadFile(filePath);
		        }

		        throw new FileNotFoundException(
			        $"The Email Path does not exist: {this._config.ConfigStore.CssEmailPath}",
			        this._config.ConfigStore.CssEmailPath);
	        }
	        case SassTypes.Head when !string.IsNullOrEmpty(this._config.ConfigStore.CssHeadPath):
	        {
		        var filePath = Path.Combine(Directory.GetCurrentDirectory(), this._config.ConfigStore.CssHeadPath);

		        if (File.Exists(filePath))
		        {
			        return ReadFile(filePath);
		        }

		        throw new FileNotFoundException(
			        $"The Email Path does not exist: {this._config.ConfigStore.CssHeadPath}",
			        this._config.ConfigStore.CssEmailPath);
	        }
        }

        // The unmodified core stylesheets ship precompiled, so only customized Sass needs compiling.
        var precompiled = this.ReadPrecompiled();

        if (precompiled is not null)
        {
            return precompiled + CompiledCssEnding;
        }

        var cachePath = Path.Combine(this._cacheDir,  this._checksum, $"{this._type}.css");

        Directory.CreateDirectory(Path.Combine(this._cacheDir, this._checksum));

        // Serialize check-then-compile-then-write per cache path so concurrent callers for the
        // same (identical) input can't observe a partially-written cache file (see CompileAndCacheScss).
        var cacheLock = CacheLocks.GetOrAdd(cachePath, static _ => new object());

        lock (cacheLock)
        {
            return (Cached(cachePath) ? ReadFile(cachePath) : this.CompileAndCacheScss(cachePath)) + CompiledCssEnding;
        }
    }

    /// <summary>
    /// Per cache-path locks guarding <see cref="Compile"/>'s check-then-compile-then-write sequence.
    /// </summary>
    private static readonly ConcurrentDictionary<string, object> CacheLocks = new();

    /// <summary>
    /// Appended to compiled css, which keeps the generated html the same as with earlier versions.
    /// </summary>
    private const string CompiledCssEnding = "\n\n";

    /// <summary>
    /// Gets the name of the embedded resource holding the precompiled css for the current Sass input.
    /// </summary>
    internal string PrecompiledResourceName =>
        $"{Constants.PrecompiledResourcePrefix}{this._type}.{this._style}.{this._checksum}.css";

    /// <summary>
    /// Reads the precompiled css for the current Sass input.
    /// </summary>
    /// <returns>The css, or <c>null</c> if the Sass input was customized.</returns>
    private string? ReadPrecompiled()
    {
        using var stream = typeof(SassCache).Assembly.GetManifestResourceStream(this.PrecompiledResourceName);

        if (stream is null)
        {
            return null;
        }

        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

	private static string ReadFile(string filePath)
	{
		using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		using var textReader = new StreamReader(fileStream);
		return textReader.ReadToEnd();
	}

    /// <summary>
    /// Loads the sass configuration.
    /// </summary>
    /// <returns>System.String.</returns>
    private string LoadSassConfig()
    {
        var sassString = this._config.SassStringFor(this._type);
        return this.ReplaceConfig(sassString);
    }

    /// <summary>
    /// Replaces the configuration.
    /// </summary>
    /// <param name="sassString">The sass string.</param>
    /// <returns>System.String.</returns>
    private string ReplaceConfig(string sassString)
    {
        return sassString.Replace("@use 'scss", $"@use '{this._config.SassLocation()}scss").Replace("@use \"scss", $"@use \"{this._config.SassLocation()}scss");
    }

    /// <summary>
    /// Check the Check sums
    /// </summary>
    /// <returns>System.String.</returns>
    private string ChecksumFiles()
    {
        var checkSums = new List<string> { GetChecksum(this._sassConfig) };

        foreach (var path in this._config.SassLoadPaths()
                     .Select(
                         loadPath => Directory.EnumerateFiles(loadPath, "*.scss", SearchOption.AllDirectories).ToList())
                     .SelectMany(files => files))
        {
            checkSums.Add(GetChecksum(File.ReadAllText(path)));
        }

        // File enumeration order differs between platforms, but the checksum has to stay stable
        // because it identifies the precompiled css.
        checkSums.Sort(StringComparer.Ordinal);

        return GetChecksum(string.Join(string.Empty, checkSums));
    }

    /// <summary>
    /// Check if the cache exists
    /// </summary>
    /// <param name="cachePath">The cache path.</param>
    /// <returns><c>true</c> Cache exist, <c>false</c> otherwise.</returns>
    private static bool Cached(string cachePath)
    {
        return File.Exists(cachePath);
    }

	/// <summary>
	/// Compiles and cache SCSS.
	/// </summary>
	/// <param name="cachePath">The cache path.</param>
	/// <returns>System.String.</returns>
	private string CompileAndCacheScss(string cachePath)
    {
		var result = this.CompileScss();

		// FileMode.Create truncates any existing (e.g. stale/partial) file instead of only
		// overwriting its leading bytes, and the caller's per-cachePath lock already prevents
		// concurrent writers/readers from observing this file mid-write.
		using (var fs = new FileStream(cachePath, FileMode.Create, FileAccess.Write))
		{
			var dataAsByteArray = new UTF8Encoding(true).GetBytes(result.CompiledContent);
			fs.Write(dataAsByteArray, 0, dataAsByteArray.Length);
		}

		if (!this._config.ConfigStore.sass_log_enabled)
        {
	        return result.CompiledContent;
        }

        Console.WriteLine($"New css file cached for {this._type}");

        foreach (var message in result.Warnings)
        {
	        Console.WriteLine(message.Message);
        }

        return result.CompiledContent;
    }

    /// <summary>
    /// Compiles the Sass input.
    /// </summary>
    /// <returns>The compilation result.</returns>
    internal CompilationResult CompileScss()
    {
        using var compiler = new SassCompiler(new JintJsEngineFactory());

        // The working directory is a load path because the Sass input is passed as a string,
        // so its "@use" rules have no file location to be resolved against.
        return compiler.Compile(
            this._sassConfig,
            false,
            new CompilationOptions
            {
                OutputStyle = this._style == StyleType.Compressed ? OutputStyle.Compressed : OutputStyle.Expanded,
                IncludePaths = [.. this._config.SassLoadPaths(), Directory.GetCurrentDirectory()]
            });
    }

    /// <summary>
    /// Gets the checksum.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>System.String.</returns>
    private static string GetChecksum(string input)
    {
        var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        return BitConverter.ToString(hashBytes).Replace("-", string.Empty).ToLower();
    }
}