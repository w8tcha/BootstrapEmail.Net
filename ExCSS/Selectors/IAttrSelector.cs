using ExCSS.Model;

namespace ExCSS.Selectors
{
    public interface IAttrSelector : ISelector
    {
        string Attribute { get;  }
        string Value { get; }
    }
}