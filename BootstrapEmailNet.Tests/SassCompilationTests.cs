using System.Runtime.CompilerServices;
using System.Text;

namespace BootstrapEmail.Net.Tests;

/// <summary>
/// Class SassCompilationTests.
/// </summary>
public class SassCompilationTests
{
    /// <summary>
    /// Set this environment variable to rewrite the precompiled css, e.g. after changing the core
    /// Sass files or updating DartSassHost, then rebuild so that the new files get embedded.
    /// </summary>
    private const string UpdateVariable = "BOOTSTRAP_EMAIL_UPDATE_PRECOMPILED";

    /// <summary>
    /// The embedded css must be exactly what the core Sass files compile to.
    /// </summary>
    [TestCase(SassTypes.SassEmail, StyleType.Expanded)]
    [TestCase(SassTypes.Head, StyleType.Compressed)]
    public void PrecompiledCssMatchesCoreSass(SassTypes type, StyleType style)
    {
        var cache = new SassCache(type, new Config(new ConfigStore()), style);
        var compiled = cache.CompileScss().CompiledContent.TrimStart('\uFEFF');

        if (Environment.GetEnvironmentVariable(UpdateVariable) == "1")
        {
            WritePrecompiled(type, style, cache.PrecompiledResourceName, compiled);
            Assert.Pass("Precompiled css updated, rebuild to embed it.");
        }

        using var stream = typeof(SassCache).Assembly.GetManifestResourceStream(cache.PrecompiledResourceName);

        stream.Should().NotBeNull($"the core Sass files changed, regenerate the css by setting {UpdateVariable}=1");

        using var reader = new StreamReader(stream!);

        reader.ReadToEnd().Should().Be(compiled, $"the compiled css changed, regenerate it by setting {UpdateVariable}=1");
    }

    /// <summary>
    /// Customized Sass must be compiled and cached instead of using the precompiled css.
    /// </summary>
    [Test]
    public void CustomSassIsCompiledAndCached()
    {
        var directory = Directory.CreateTempSubdirectory("bootstrap-email-tests").FullName;

        try
        {
            File.WriteAllText(
                Path.Combine(directory, "custom.scss"),
                "@use 'scss/variable';\n.custom { color: red; }\n");

            var config = new Config(
                new ConfigStore
                {
                    sass_email_location = directory,
                    sass_email_string = "custom.scss",
                    sass_cache_location = Path.Combine(directory, "cache")
                });

            var css = SassCache.Compile(SassTypes.SassEmail, config, StyleType.Expanded);

            css.Should().Contain(".custom").And.Contain("color: red");
            Directory.GetFiles(Path.Combine(directory, "cache"), "*.css", SearchOption.AllDirectories)
                .Should().ContainSingle();
        }
        finally
        {
            Directory.Delete(directory, true);
        }
    }

    /// <summary>
    /// Replaces the precompiled css in the source tree.
    /// </summary>
    private static void WritePrecompiled(
        SassTypes type,
        StyleType style,
        string resourceName,
        string css,
        [CallerFilePath] string testFile = "")
    {
        var directory = Path.GetFullPath(
            Path.Combine(Path.GetDirectoryName(testFile)!, "..", "BootstrapEmailNet", "precompiled"));

        Directory.CreateDirectory(directory);

        foreach (var file in Directory.GetFiles(directory, $"{type}.{style}.*.css"))
        {
            File.Delete(file);
        }

        File.WriteAllText(
            Path.Combine(directory, resourceName[Constants.PrecompiledResourcePrefix.Length..]),
            css,
            new UTF8Encoding(false));
    }
}
