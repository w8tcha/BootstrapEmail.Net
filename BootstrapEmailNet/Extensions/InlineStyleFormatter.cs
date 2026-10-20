using CompressedStyleFormatter = ExCSS.CompressedStyleFormatter;
using IStyleFormattable = ExCSS.IStyleFormattable;
using IStyleFormatter = ExCSS.IStyleFormatter;

namespace BootstrapEmail.Net.Extensions;

/// <summary>
/// Compressed ExCSS formatter for style attributes.
/// Joins declarations without a space and writes colors as hex instead of rgb()/rgba().
/// </summary>
internal sealed partial class InlineStyleFormatter : IStyleFormatter
{
    public static readonly IStyleFormatter Instance = new InlineStyleFormatter();

    private static readonly IStyleFormatter Compressed = CompressedStyleFormatter.Instance;

    public string Declaration(string name, string value, bool important) => Compressed.Declaration(name, value, important);

    public string Declarations(IEnumerable<string> declarations)
    {
        // shorthands are always serialized with CompressedStyleFormatter, so convert colors on the joined result
        return ToHexColors(string.Join(";", declarations));
    }

    public string Sheet(IEnumerable<IStyleFormattable> rules) => Compressed.Sheet(rules);

    public string Block(IEnumerable<IStyleFormattable> rules) => Compressed.Block(rules);

    public string Medium(bool exclusive, bool inverse, string type, IEnumerable<IStyleFormattable> constraints) =>
        Compressed.Medium(exclusive, inverse, type, constraints);

    public string Constraint(string name, string value, string constraintDelimiter) =>
        Compressed.Constraint(name, value, constraintDelimiter);

    public string Rule(string name, string value) => Compressed.Rule(name, value);

    public string Rule(string name, string prelude, string rules) => Compressed.Rule(name, prelude, rules);

    public string Style(string selector, IStyleFormattable rules) => Compressed.Style(selector, rules);

    public string Comment(string data) => Compressed.Comment(data);

    /// <summary>
    /// Replaces the rgb()/rgba() output of ExCSS colors with hex. Alpha is dropped.
    /// </summary>
    internal static string ToHexColors(string value)
    {
        if (string.IsNullOrEmpty(value) || !value.Contains("rgb", StringComparison.Ordinal))
        {
            return value;
        }

        return RgbRegex().Replace(
            value,
            m => $"#{byte.Parse(m.Groups[1].Value):x2}{byte.Parse(m.Groups[2].Value):x2}{byte.Parse(m.Groups[3].Value):x2}");
    }

    [GeneratedRegex(@"rgba?\((\d{1,3}), (\d{1,3}), (\d{1,3})(?:, [^)]*)?\)")]
    private static partial Regex RgbRegex();
}
