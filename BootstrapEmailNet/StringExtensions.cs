namespace BootstrapEmail.Net;

/// <summary>
/// Class StringExtensions.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Truncates a string with the specified limits and adds (...) to the end if truncated
    /// </summary>
    /// <param name="input">
    /// input string
    /// </param>
    /// <param name="inputLimit">
    /// The input Limit.
    /// </param>
    /// <param name="cutOfString">
    /// The cut Of String.
    /// </param>
    /// <returns>
    /// truncated string
    /// </returns>
    public static string Truncate(
        this string input,
        int inputLimit,
        string cutOfString = "...")
    {
        var output = input;

        if (string.IsNullOrWhiteSpace(input))
        {
            return string.Empty;
        }

        var limit = inputLimit - cutOfString.Length;

        // Check if the string is longer than the allowed amount
        // otherwise do nothing
        if (output.Length <= limit || limit <= 0)
        {
            return output;
        }

        // cut the string down to the maximum number of characters
        output = output[..limit];

        // Check if the space right after the truncate point
        // was a space. if not, we are in the middle of a word and
        // need to cut out the rest of it
        if (input.Substring(output.Length, 1) != " ")
        {
            var lastSpace = output.LastIndexOf(' ');

            // if we found a space then, cut back to that space
            if (lastSpace != -1)
            {
                output = output[..lastSpace];
            }
        }

        // Finally, add the cutoff string...
        output += cutOfString;

        return output;
    }
}