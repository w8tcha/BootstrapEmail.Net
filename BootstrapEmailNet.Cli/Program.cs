namespace BootstrapEmail.Net.Cli;

using System.Text.Json;

using CommandLine;
using CommandLine.Text;

/// <summary>
/// Class Program.
/// </summary>
internal class Program
{
    /// <summary>
    /// Defines the entry point of the application.
    /// </summary>
    /// <param name="args">The arguments.</param>
    private static void Main(string[] args)
    {
        var parser = new Parser(with => with.HelpWriter = null);
        var parserResult = parser.ParseArguments<ConsoleOptions>(args);

        parserResult.WithParsed(options => Run(options, parserResult))
            .WithNotParsed(_ => DisplayHelp(parserResult));
    }

    /// <summary>
    /// Displays the help.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="result">The result.</param>
    private static void DisplayHelp<T>(ParserResult<T> result)
    {
        var helpText = HelpText.AutoBuild(
            result,
            h =>
                {
                    h.AdditionalNewLineAfterOption = false;

                    h.Heading =
                        "Bootstrap 5 stylesheet, compiler, and in-liner for responsive and consistent emails with the Bootstrap syntax you know and love.";

                    h.AddPostOptionsLine("Usage: BootstrapEmail.Cli <path> [options]");
                    h.AddPostOptionsLine("");
                    h.AddPostOptionsLine("Examples:");
                    h.AddPostOptionsLine("  BootstrapEmail.Cli");
                    h.AddPostOptionsLine("  BootstrapEmail.Cli email.html > out.html");
                    h.AddPostOptionsLine("  BootstrapEmail.Cli ./public/index.html");
                    h.AddPostOptionsLine(
                        "  BootstrapEmail.Cli -s '<a href=\"#\" class=\"btn btn-primary\">Some Button</a>'");
                    h.AddPostOptionsLine("  BootstrapEmail.Cli -p 'emails/*' -d emails/compiled");
                    h.AddPostOptionsLine("  BootstrapEmail.Cli -p 'views/emails/*' -d 'views/compiled_emails'");
                    h.AddPostOptionsLine("  cat input.html | BootstrapEmail.Cli");

                    return HelpText.DefaultParsingErrorsHandler(result, h);
                },
            e => e);
        Console.WriteLine(helpText);
    }

    /// <summary>
    /// Runs the specified options.
    /// </summary>
    /// <param name="options">The options.</param>
    /// <param name="parserResult">The parser result.</param>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    private static void Run(ConsoleOptions options, ParserResult<ConsoleOptions> parserResult)
    {
        var input = string.Empty;
        var output = string.Empty;
        var type = InputType.File;

        //do stuff
        if (options.Version)
        {
            var name = typeof(BootstrapEmail).Assembly.GetName();

            Console.WriteLine(name.Version);
            Environment.Exit(0);
        }
        else if (options.Help)
        {
            DisplayHelp(parserResult);
            Environment.Exit(0);
        }
        else if (options.File is not null)
        {
            input = string.Join(" ", options.File);
            
            if (!string.IsNullOrEmpty(options.Destination))
            {
                output = string.Join(" ", options.Destination);
            }

            type = InputType.File;
        }
        else if (options.String is not null)
        {
            input = string.Join(" ", options.String);
            type = InputType.String;
        }
        else if (options.Pattern is not null && options.Destination is not null)
        {
            input = string.Join(" ", options.Pattern).Replace("'", "");
            output = string.Join(" ", options.Destination).Replace("'", "");

            type = InputType.Pattern;
        }

        var config = new ConfigStore();

        if (options.Config is not null)
        {
            options.Config = Path.GetFullPath(options.Config, Directory.GetCurrentDirectory());

            var jsonString = File.ReadAllText(options.Config);
            config = JsonSerializer.Deserialize<ConfigStore>(jsonString)!;
        }

        var bsEmail = new BootstrapEmail(config);

        if (!string.IsNullOrEmpty(input))
        {
            switch (type)
            {
                case InputType.Pattern:
                    bsEmail.Compile(input, output, InputType.Pattern);

                    break;
                case InputType.File:
                    var filePath = Path.GetFullPath(input, Directory.GetCurrentDirectory());

                    bsEmail.Compile(filePath,  output, InputType.File);

                    break;
                case InputType.String:
                    Console.WriteLine(bsEmail.Compile(input,string.Empty,type));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        else
        {
            // compile all files ending in .html in the current directory
            bsEmail.Compile(input, output, InputType.File);
        }
    }
}