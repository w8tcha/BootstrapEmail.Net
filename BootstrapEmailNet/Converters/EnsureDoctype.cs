namespace BootstrapEmail.Net.Converters;

/// <summary>
/// Class EnsureDoctype.
/// </summary>
public static class EnsureDoctype
{
	/// <summary>
	/// Replaces the specified HTML.
	/// </summary>
	/// <param name="html">The HTML.</param>
	/// <returns>System.String.</returns>
	public static string Replace(string html)
	{
		const string docType =
			"<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">";

		// ensure the proper XHTML doctype which ensures the best compatibility in email clients
		// https://github.com/bootstrap-email/bootstrap-email/discussions/168
		var docTypeRegex =
			new Regex(@"^<!DOCTYPE.*(\[[\s\S]*?\])?>", RegexOptions.None, TimeSpan.FromMilliseconds(100));

		return !docTypeRegex.IsMatch(html) ? $"{docType}\n{html}" : html;
	}
}