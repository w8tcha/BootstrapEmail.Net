

namespace BootstrapEmail.Net.Converters;

using System.Text;

public class Stack : Base
{
    public Stack(IHtmlDocument document)
        : base(document)
    {
    }

    public virtual void Build()
    {
        this.StackRow();
        this.StackCol();
    }

    private void StackRow()
    {
        foreach (var node in this.EachNode(".stack-row"))
        {
            var html = new StringBuilder();

            foreach (var child in node.Children)
            {
                Dictionary<string, object> templateContent = new()
                                                                 {
                                                                     { "classes", "stack-cell" },
                                                                     { "contents", child.OuterHtml }
                                                                 };

                html.Append(this.Template("td", templateContent));
            }

            Dictionary<string, object> content = new()
                                                     {
                                                         { "classes", node.ClassName ?? string.Empty }, { "contents", html.ToString() }
                                                     };

            node.OuterHtml = this.Template("table-to-tr", content);
        }
    }

    private void StackCol()
    {
        foreach (var node in this.EachNode(".stack-col"))
        {
            var html = new StringBuilder();

            foreach (var child in node.Children)
            {
                Dictionary<string, object> templateContent = new()
                                                                 {
                                                                     { "classes", "stack-cell" },
                                                                     { "contents", child.OuterHtml }
                                                                 };

                html.Append(this.Template("tr", templateContent));
            }

            Dictionary<string, object> content = new()
                                                     {
                                                         { "classes", node.ClassName ?? string.Empty }, { "contents", html.ToString() }
                                                     };

            node.OuterHtml = this.Template("table-to-tbody", content);
        }
    }
}