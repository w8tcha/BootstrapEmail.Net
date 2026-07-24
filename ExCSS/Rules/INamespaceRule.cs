namespace ExCSS.Rules
{
    public interface INamespaceRule : IRule
    {
        string NamespaceUri { get; set; }
        string Prefix { get; set; }
    }
}