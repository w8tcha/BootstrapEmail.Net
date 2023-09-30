/*using System;
using System.Text.RegularExpressions;

namespace BootstrapEmailNet.Converters;

public class SupportUrlTokens : Base
{
    private static readonly string OPEN_BRACKETS = Uri.EscapeDataString("{{");
    private static readonly string OPEN_PERCENT = Uri.EscapeDataString("{") + "%";
    private static readonly string CLOSE_BRACKETS = Uri.EscapeDataString("}}");
    private static readonly string CLOSE_PERCENT = "%" + Uri.EscapeDataString("}");

    public static void Replace(ref string html)
    {
        Regex regex = new Regex(@"((href|src)=(""|'))(.*?((\"" + Regex.Escape(OPEN_BRACKETS) + @" |\"" + Regex.Escape(OPEN_PERCENT) + @").*?(" + Regex.Escape(CLOSE_BRACKETS) + @"|\"" + Regex.Escape(CLOSE_PERCENT) + @")).*?)("" | ')");
        if (!regex.IsMatch(html))
        {
            return;
        }
        Regex innerRegex = new Regex(@"((\"" + Regex.Escape(OPEN_BRACKETS) + @" |\"" + Regex.Escape(OPEN_PERCENT) + @").*?(" + Regex.Escape(CLOSE_BRACKETS) + @"|\"" + Regex.Escape(CLOSE_PERCENT) + @"))");
        html = regex.Replace(html, match =>
            {
                string startText = match.Groups[1].Value;
                string middleText = match.Groups[4].Value;
                string endText = match.Groups[8].Value;
                middleText = innerRegex.Replace(middleText, m => Uri.UnescapeDataString(m.Value));
                return startText + middleText + endText;
            });
    }
}*/