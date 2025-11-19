using System.Text;

namespace BootstrapEmail.Net;

using System;
using System.IO;
using System.Security.Cryptography;

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
		        var filePath = Path.Combine(AppContext.BaseDirectory, this._config.ConfigStore.CssEmailPath);

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
		        var filePath = Path.Combine(AppContext.BaseDirectory, this._config.ConfigStore.CssHeadPath);

		        if (File.Exists(filePath))
		        {
			        return ReadFile(filePath);
		        }

		        throw new FileNotFoundException(
			        $"The Email Path does not exist: {this._config.ConfigStore.CssHeadPath}",
			        this._config.ConfigStore.CssEmailPath);
	        }
        }

        var cachePath = Path.Combine(this._cacheDir,  this._checksum, $"{this._type}.css");

        Directory.CreateDirectory(Path.Combine(this._cacheDir, this._checksum));

		return Cached(cachePath) ? ReadFile(cachePath) : this.CompileAndCacheScss(cachePath);
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
        var checkSums = new[] { GetChecksum(this._sassConfig) };

        foreach (var path in this._config.SassLoadPaths()
                     .Select(
                         loadPath => Directory.EnumerateFiles(loadPath, "*.scss", SearchOption.AllDirectories).ToList())
                     .SelectMany(files => files))
        {
            _ = checkSums.Append(GetChecksum(File.ReadAllText(path)));
        }

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
	/// The lock
	/// </summary>
	private static readonly ReaderWriterLockSlim Lock = new();

	/// <summary>
	/// Compiles and cache SCSS.
	/// </summary>
	/// <param name="cachePath">The cache path.</param>
	/// <returns>System.String.</returns>
	private string CompileAndCacheScss(string cachePath)
    {
	    var compiler = this._config.ConfigStore.DartSassNativeType.HasValue
		    ? new DartSassCompiler(this._config.ConfigStore.DartSassNativeType.Value)
		    : new DartSassCompiler();

		var result = compiler.CompileCodeAsync(this._sassConfig,
		    new SassCompileOptions { StyleType = this._style, StopOnError = true }).Result;

		Lock.EnterWriteLock();

		try
		{
			using var fs = new FileStream(cachePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
			var dataAsByteArray = new UTF8Encoding(true).GetBytes(result.Code);
			fs.Write(dataAsByteArray, 0, result.Code.Length);
		}
		finally
		{
			Lock.ExitWriteLock();
		}

		if (!this._config.ConfigStore.sass_log_enabled)
        {
	        return result.Code;
        }

        Console.WriteLine($"New css file cached for {this._type}");

        foreach (var message in result.Warnings)
        {
	        Console.WriteLine(message.Message);
        }

        return result.Code;
    }

    /// <summary>
    /// Gets the checksum.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>System.String.</returns>
    private static string GetChecksum(string input)
    {
        var hashBytes = SHA1.HashData(Encoding.UTF8.GetBytes(input));
        return BitConverter.ToString(hashBytes).Replace("-", string.Empty).ToLower();
    }
}