namespace BootstrapEmail.Net.Cli;

using System.Text;
using System.Text.Json;

using CommandLine;
using CommandLine.Text;

/// <summary>
/// Class Program.
/// </summary>
public static class Program
{
    /// <summary>
    /// Defines the entry point of the application.
    /// </summary>
    /// <param name="args">The arguments.</param>
    private static void Main(string[] args)
    {
        // The default encoding on Windows can't represent all characters of the compiled emails.
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;

        var parser = new Parser(
            with =>
                {
                    with.HelpWriter = null;
                    with.AutoHelp = false;
                    with.AutoVersion = false;
                });
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
        var helpText = new HelpText
        {
            AddDashesToOption = true,
            AdditionalNewLineAfterOption = false,
            AutoHelp = false,
            AutoVersion = false,
            Heading =
                "Bootstrap 5 stylesheet, compiler, and in-liner for responsive and consistent emails with the Bootstrap syntax you know and love."
        };

        if (result.Tag == ParserResultType.NotParsed)
        {
            HelpText.DefaultParsingErrorsHandler(result, helpText);
        }

        helpText.AddOptions(result);

        helpText.AddPostOptionsLine("Usage: bootstrap-email [options]");
        helpText.AddPostOptionsLine("");
        helpText.AddPostOptionsLine("Examples:");
        helpText.AddPostOptionsLine("  bootstrap-email");
        helpText.AddPostOptionsLine("  bootstrap-email -f email.html -d out.html");
        helpText.AddPostOptionsLine("  bootstrap-email -s '<a href=\"#\" class=\"btn btn-primary\">Some Button</a>'");
        helpText.AddPostOptionsLine("  bootstrap-email -p 'emails/*' -d emails/compiled");
        helpText.AddPostOptionsLine("  bootstrap-email -t -f email.html -d email.txt");
        helpText.AddPostOptionsLine("  cat input.html | bootstrap-email");

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
            var name = typeof(BootstrapEmailCompiler).Assembly.GetName();

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
        else if (options.Pattern is not null)
        {
            Console.Error.WriteLine("--pattern requires --destination.");
            Environment.Exit(1);
        }
        else if (Console.IsInputRedirected)
        {
            input = Console.In.ReadToEnd();
            type = InputType.String;
        }

        var config = new ConfigStore();

        if (options.Config is not null)
        {
            options.Config = Path.GetFullPath(options.Config, Directory.GetCurrentDirectory());

            var jsonString = File.ReadAllText(options.Config);
            config = JsonSerializer.Deserialize<ConfigStore>(jsonString)!;
        }

        config.plain_text |= options.Text;

        var bsEmail = new BootstrapEmailCompiler(config);

        if (!string.IsNullOrEmpty(input))
        {
            switch (type)
            {
                case InputType.Pattern:
                    bsEmail.Compile(input, output, InputType.Pattern);

                    break;
                case InputType.File:
                    var filePath = Path.GetFullPath(input, Directory.GetCurrentDirectory());

                    bsEmail.Compile(filePath, output, InputType.File);

                    break;
                default:
                    Console.WriteLine(bsEmail.Compile(input, string.Empty, type));
                    break;
            }
        }
        else
        {
            // compile all files ending in .html in the current directory
            bsEmail.Compile(input, output, InputType.File);
        }
    }
}