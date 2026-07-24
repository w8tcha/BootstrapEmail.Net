using ExCSS.Model;

namespace ExCSS.StyleProperties
{
    public interface IProperty : IStylesheetNode
    {
        string Name { get; }
        string Value { get; }
        string Original { get; }
        bool IsImportant { get; }
    }
}