using System.IO;

namespace ExCSS;

public static class FormatExtensions
{
    extension(IStyleFormattable style)
    {
        public string ToCss()
        {
            return style.ToCss(CompressedStyleFormatter.Instance);
        }

        public string ToCss(IStyleFormatter formatter)
        {
            var sb = Pool.NewStringBuilder();
            using (var writer = new StringWriter(sb))
            {
                style.ToCss(writer, formatter);
            }

            return sb.ToPool();
        }

        public void ToCss(TextWriter writer)
        {
            style.ToCss(writer, CompressedStyleFormatter.Instance);
        }
    }
}