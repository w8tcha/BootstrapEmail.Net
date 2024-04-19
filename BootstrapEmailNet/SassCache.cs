using System.Text;

using Citizen17.DartSass;

namespace BootstrapEmail.Net;

using System;
using System.Security.Cryptography;

/// <summary>
/// Class SassCache.
/// </summary>
public class SassCache
{
    private readonly string type;

    private readonly Config config;

    private readonly StyleType style;

    private readonly string sassConfig;

    private readonly string checksum;

    private readonly string cacheDir;

    /// <summary>
    /// Compiles the specified type.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="config">The configuration.</param>
    /// <param name="style">The style.</param>
    /// <returns>System.String.</returns>
    public static string Compile(string type, Config config, StyleType style = StyleType.Compressed)
    {
        return new SassCache(type, config, style).Compile();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SassCache"/> class.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="config">The configuration.</param>
    /// <param name="style">The style.</param>
    public SassCache(string type, Config config, StyleType style)
    {
        this.type = type;
        this.config = config;
        this.style = style;
        this.sassConfig = this.LoadSassConfig();
        this.checksum = this.ChecksumFiles();
        this.cacheDir = config.SassCacheLocation();
	}

	/// <summary>
	/// Compile
	/// </summary>
	/// <returns>System.String.</returns>
    public string Compile()
    {
        var cachePath = Path.Combine(this.cacheDir,  this.checksum, $"{this.type}.css");
        var lockPath = Path.Combine(this.cacheDir, this.checksum, $"{this.type}css.lock");

        Directory.CreateDirectory(Path.Combine(this.cacheDir, this.checksum));

        using var lockFile = File.Open(lockPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);

        return Cached(cachePath) ? File.ReadAllText(cachePath) : this.CompileAndCacheScss(cachePath);
    }

    /// <summary>
    /// Loads the sass configuration.
    /// </summary>
    /// <returns>System.String.</returns>
    private string LoadSassConfig()
    {
        var sassString = this.config.SassStringFor(type: this.type);
        return this.ReplaceConfig(sassString);
    }

    /// <summary>
    /// Replaces the configuration.
    /// </summary>
    /// <param name="sassString">The sass string.</param>
    /// <returns>System.String.</returns>
    private string ReplaceConfig(string sassString)
    {
        return sassString.Replace("@import 'scss", $"@import '{this.config.SassLocation()}scss");
    }

    /// <summary>
    /// Check the Check sums
    /// </summary>
    /// <returns>System.String.</returns>
    private string ChecksumFiles()
    {
        var checkSums = new[] { GetChecksum(this.sassConfig) };

        foreach (var path in this.config.SassLoadPaths()
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
    /// Compiles and cache SCSS.
    /// </summary>
    /// <param name="cachePath">The cache path.</param>
    /// <returns>System.String.</returns>
    private string CompileAndCacheScss(string cachePath)
    {
	    var result = new DartSassCompiler().CompileCodeAsync(this.sassConfig,
		    new SassCompileOptions { StyleType = this.style, StopOnError = true }).Result;

        File.WriteAllText(cachePath, result.Code);

        if (!this.config.SassLogEnabled())
        {
	        return result.Code;
        }

        Console.WriteLine($"New css file cached for {this.type}");

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