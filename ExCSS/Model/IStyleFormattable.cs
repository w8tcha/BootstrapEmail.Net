using System.IO;

namespace ExCSS.Model
{
    public interface IStyleFormattable
    {
        void ToCss(TextWriter writer, IStyleFormatter formatter);
    }
}