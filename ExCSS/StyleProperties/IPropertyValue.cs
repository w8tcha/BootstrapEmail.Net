using ExCSS.Model;

namespace ExCSS.StyleProperties
{
    internal interface IPropertyValue
    {
        string CssText { get; }
        TokenValue Original { get; }
        TokenValue ExtractFor(string name);
    }
}