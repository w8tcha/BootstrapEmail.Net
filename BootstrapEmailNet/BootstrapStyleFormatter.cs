namespace BootstrapEmail.Net;

using AngleSharp.Css;
using AngleSharp.Css.Dom;
using AngleSharp.Text;

/// <summary>
/// Represents the CSS3 markup formatter with minimal code.
/// </summary>
public class BootstrapEmailStyleFormatter : IStyleFormatter
{
    #region Properties

    /// <summary>
    /// Gets or sets if comments should be preserved.
    /// </summary>
    public bool ShouldKeepComments { get; set; }

    /// <summary>
    /// Gets or sets if empty (zero-length) rules should be kept.
    /// </summary>
    public bool ShouldKeepEmptyRules { get; set; }

    #endregion

    #region Methods

    string IStyleFormatter.Sheet(IEnumerable<IStyleFormattable> rules)
    {
        if (!ShouldKeepEmptyRules && !IsNotEmpty(rules))
        {
            return string.Empty;
        }

        var sb = StringBuilderPool.Obtain();

        using (var writer = new StringWriter(sb))
        {
            foreach (var rule in rules)
            {
                rule.ToCss(writer, this);
            }
        }

        return sb.ToPool();
    }

    string IStyleFormatter.BlockRules(IEnumerable<IStyleFormattable> rules)
    {
        if (!ShouldKeepEmptyRules && !IsNotEmpty(rules))
        {
            return string.Empty;
        }

        var sb = StringBuilderPool.Obtain().Append(Symbols.CurlyBracketOpen);

        using (var writer = new StringWriter(sb))
        {
            foreach (var rule in rules)
            {
                rule.ToCss(writer, this);
            }
        }

        return sb.Append(Symbols.CurlyBracketClose).ToPool();
    }

    string IStyleFormatter.Declaration(string name, string value, bool important) =>
        $"{name}: {string.Concat(value.ToLowerInvariant(), important ? " !important" : string.Empty)}";

    string IStyleFormatter.BlockDeclarations(IEnumerable<IStyleFormattable> declarations)
    {
        if (!ShouldKeepEmptyRules && !declarations.OfType<ICssProperty>().Any())
        {
            return string.Empty;
        }

        var sb = StringBuilderPool.Obtain().Append(Symbols.CurlyBracketOpen);

        using (var writer = new StringWriter(sb))
        {
            foreach (var declaration in declarations)
            {
                declaration.ToCss(writer, this);
                writer.Write(Symbols.Semicolon);
            }

            if (sb.Length > 1)
            {
                sb.Remove(sb.Length - 1, 1);
            }
        }

        return sb.Append(Symbols.CurlyBracketClose).ToPool();
    }

    string IStyleFormatter.Rule(string name, string value) =>
        CssStyleFormatter.Instance.Rule(name, value);

    string IStyleFormatter.Rule(string name, string prelude, string rules) =>
        string.IsNullOrEmpty(rules) ? string.Empty : string.Concat(name, string.IsNullOrEmpty(prelude) ? string.Empty : $" {prelude}", rules);

    string IStyleFormatter.Comment(string data) =>
        ShouldKeepComments ? CssStyleFormatter.Instance.Comment(data) : string.Empty;

    #endregion

    #region Helpers

    private static bool IsNotEmpty(IEnumerable<IStyleFormattable> rules)
    {
        foreach (var rule in rules.OfType<ICssRule>())
        {
            switch (rule.Type)
            {
                case CssRuleType.Document:
                case CssRuleType.Supports:
                case CssRuleType.Media:
                    if (IsNotEmpty(((ICssGroupingRule)rule).Rules))
                    {
                        return true;
                    }

                    break;
                case CssRuleType.Keyframes:
                    if (IsNotEmpty(((ICssKeyframesRule)rule).Rules))
                    {
                        return true;
                    }

                    break;
                case CssRuleType.FontFace:
                    if (((ICssFontFaceRule)rule).Style.Any())
                    {
                        return true;
                    }

                    break;
                case CssRuleType.Page:
                    if (((ICssPageRule)rule).Style.Any())
                    {
                        return true;
                    }

                    break;
                case CssRuleType.Style:
                    if (((ICssStyleRule)rule).Style.Any())
                    {
                        return true;
                    }

                    break;
                case CssRuleType.Keyframe:
                    if (((ICssKeyframeRule)rule).Style.Any())
                    {
                        return true;
                    }

                    break;
                default:
                    return true;
            }
        }

        return false;
    }

    #endregion
}