namespace BootstrapEmail.Net;

/// <summary>
/// Class BootstrapEmail.
/// </summary>
public class BootstrapEmail
{
    /// <summary>
    /// The configuration
    /// </summary>
    private readonly ConfigStore config;

    /// <summary>
    /// Initializes a new instance of the <see cref="BootstrapEmail"/> class.
    /// </summary>
    /// <param name="configStore">The configuration store.</param>
    public BootstrapEmail(ConfigStore configStore)
    {
        this.config = configStore;
    }

    /// <summary>
    /// Deletes the sass cache
    /// </summary>
    public void ClearSassCache()
    {
        var sassCacheLocation = new Config(this.config).SassCacheLocation();

        if (Directory.Exists(sassCacheLocation))
        {
            Directory.Delete(sassCacheLocation, true);
        }
    }

    /// <summary>
    /// Compiles the specified input.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="output">The output.</param>
    /// <param name="type">The type.</param>
    /// <returns>System.String.</returns>
    public string Compile(string input, string output, InputType type)
    {
        switch (type)
        {
            case InputType.Pattern:
                this.PatternCompile(input, output);

                break;
            case InputType.File:
                if (string.IsNullOrEmpty(input))
                {
                    this.FilesCompile();
                }
                else
                {
                    this.FileCompile(input, output);
                }

                break;
            case InputType.String:
                return this.StringCompile(input);
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        return string.Empty;
    }

    /// <summary>
    /// specify a path pattern and a destination directory for compiled emails to be saved to
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="output">The output.</param>
    private void PatternCompile(string input, string output)
    {
        foreach (var path in Directory.EnumerateFiles(AppContext.BaseDirectory, input))
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"File does not exist: {path}");

                continue;
            }

            Console.WriteLine($"Compiling file...{path}");

            var compilerPattern = new Compiler(path, this.config, InputType.Pattern);

            var convertedHtml = compilerPattern.PerformFullCompile();

            var destinationPath = Path.Combine(AppContext.BaseDirectory, output.TrimEnd('*'));

            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            File.WriteAllText(
                Path.Combine(destinationPath, Path.GetFileName(path)),
                convertedHtml);
        }
    }

    /// <summary>
    /// compile for a string
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>System.String.</returns>
    private string StringCompile(string input)
    {
        var compilerString = new Compiler(input, config: this.config);

        return
            compilerString.PerformFullCompile();
    }

    /// <summary>
    /// compile the file
    /// </summary>
    /// <param name="relativeFilePath">The relative file path.</param>
    /// <param name="savePath">The save path.</param>
    private void FileCompile(string relativeFilePath, string? savePath)
    {
        var filePath = Path.GetFullPath(relativeFilePath, AppContext.BaseDirectory);

        var compilerFile = new Compiler(filePath, this.config, InputType.File);

        var compiledHtml = compilerFile.PerformFullCompile();

        if (string.IsNullOrEmpty(savePath))
        {
            var folderName = Path.GetDirectoryName(filePath);
            var fileName = Path.GetFileName(filePath);

            if (folderName != null)
            {
                var targetFolderPath = Path.Combine(folderName, Constants.Compiled);

                if (!Directory.Exists(targetFolderPath))
                {
                    Directory.CreateDirectory(targetFolderPath);
                }

                savePath = Path.Combine(targetFolderPath, fileName);
            }
            else
            {
                savePath = Path.Combine(filePath, fileName);
            }
        }
        else
        {
            savePath = Path.GetFullPath(savePath, Directory.GetCurrentDirectory());
        }

        // Save compiled html
        File.WriteAllText(savePath, compiledHtml);
    }

    /// <summary>
    /// compile all files ending in .html in the current directory
    /// </summary>
    private void FilesCompile()
    {
        var files = Directory.GetFiles(AppContext.BaseDirectory, "*.html");

        foreach (var file in files)
        {
            this.Compile(file, string.Empty, InputType.File);
        }
    }
}