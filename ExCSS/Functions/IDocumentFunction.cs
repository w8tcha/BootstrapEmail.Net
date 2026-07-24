using ExCSS.Model;

namespace ExCSS.Functions
{
    public interface IDocumentFunction : IStylesheetNode
    {
        string Name { get; }
        string Data { get; }
    }
}