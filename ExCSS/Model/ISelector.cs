namespace ExCSS.Model
{
    public interface ISelector : IStylesheetNode
    {
        Priority Specificity { get; }
        string Text { get; }
    }
}