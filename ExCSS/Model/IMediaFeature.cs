namespace ExCSS.Model
{
    public interface IMediaFeature : IStylesheetNode
    {
        string Name { get; }
        string Value { get; }
        bool HasValue { get; }
    }
}