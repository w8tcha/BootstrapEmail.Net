namespace BootstrapEmail.Net.Cli;

using CommandLine;

/// <summary>
/// Class Console Options.
/// </summary>
public class ConsoleOptions
{
    /// <summary>
    /// Set output to verbose messages.
    /// </summary>
    [Option('h', "help", Required = false, HelpText = "Set output to verbose messages.")]
    public bool Help { get; set; }

    /// <summary>
    /// Relative path to JSON config file to customize Bootstrap Email.
    /// </summary>
    [Option('c', "string", Required = false, HelpText = "Relative path to JSON config file to customize Bootstrap Email.")]
    public string? Config { get; set; }

    /// <summary>
    /// Return the plain text version of the email.
    /// </summary>
    [Option('t', "text", Required = false, HelpText = "Return the plain text version of the email.")]
    public string? Text { get; set; }

    /// <summary>
    /// HTML string to be compiled rather than a file.
    /// </summary>
    [Option(
        's',
        "string",
        Required = false,
        HelpText = "HTML string to be to be compiled rather than a file.")]
    public string? String { get; set; }

    /// <summary>
    /// File to be compiled.
    /// </summary>
    [Option('f', "string", Required = false, HelpText = "File to be compiled.")]
    public string? File { get; set; }

    /// <summary>
    /// Specify a pattern of files to compile and save multiple files at once.
    /// </summary>
    [Option(
        'p',
        "pattern",
        Required = false,
        HelpText = "Specify a pattern of files to compile and save multiple files at once")]
    public string? Pattern { get; set; }

    /// <summary>
    /// Destination for compiled files (used with the --pattern option).
    /// </summary>
    [Option(
        'd',
        "destination",
        Required = false,
        HelpText = "Destination for compiled files (used with the --pattern option).")]
    public string? Destination { get; set; }

    /// <summary>
    /// Show version
    /// </summary>
    [Option('v', "version", HelpText = "Show version.")]
    public bool Version { get; set; }
}