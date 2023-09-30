namespace BootstrapEmail.Net.Converters;

public class Body : Base
{
    public Body(IHtmlDocument document) : base(document)
    {
    }

    public virtual void Build()
    {
        var body = this.Document.QuerySelector("body");

        Dictionary<string, object> templateContent = new()
                                                         {
                                                             { "classes", $"{body.GetAttribute("class")}body" },
                                                             {
                                                                 "contents",
                                                                 body.InnerHtml.Replace("\n", string.Empty)
                                                             }
                                                         };
        body.InnerHtml = $"\r\n    {this.Template("body", templateContent)}";
    }
}