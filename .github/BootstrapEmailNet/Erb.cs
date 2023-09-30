namespace BootstrapEmail.Net;

/// <summary>
/// Class Erb.
/// </summary>
public class Erb
{
    /// <summary>
    /// The main layout template
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="contents">The contents.</param>
    /// <returns>System.String.</returns>
    public static string Template(string path, string contents)
    {
        var templateHtml = File.ReadAllText(path);

        return templateHtml.Replace("<%= contents %>", contents);
    }
}