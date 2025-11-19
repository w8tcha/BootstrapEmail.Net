namespace BootstrapEmail.Net.Converters;

/// <summary>
/// Class ForceEncoding.
/// </summary>
public static class ForceEncoding
{
	/// <summary>
	/// Replaces the specified HTML.
	/// </summary>
	/// <param name="html">The HTML.</param>
	/// <returns>System.String.</returns>
	public static string Replace(string html)
    {
        // force utf-8 character encoded in iOS Mail: https://github.com/bootstrap-email/bootstrap-email/issues/50
        // this needs to be done after the document has been outputted to an ascii string so it doesn't get converted
        return html.Replace(
            "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=US-ASCII\">",
            "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
    }
}