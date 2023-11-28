namespace BootstrapEmail.Net.Converters;

public class Body : Base
{
    public Body(IHtmlDocument document, Config config)
        : base(document, config)
    {
    }

    public virtual void Build()
    {
        var body = this.Document.QuerySelector("body");

        if (body == null)
        {
            return;
        }

        var templateContent = new TemplateContent(
            $"{body.GetAttribute("class")}body",
            body.InnerHtml.Replace("\n", string.Empty));

        body.InnerHtml = $"\r\n    {this.Template("body", templateContent)}";
    }
}