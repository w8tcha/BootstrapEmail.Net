namespace BootstrapEmail.Net.Objects;

/// <summary>
/// Class TemplateContent.
/// </summary>
public class TemplateContent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TemplateContent"/> class.
    /// </summary>
    /// <param name="classes">The classes.</param>
    /// <param name="contents">The contents.</param>
    public TemplateContent(string classes, string contents)
    {
        this.Classes = classes;
        this.Contents = contents;
    }

    /// <summary>
    /// Gets or sets the classes.
    /// </summary>
    /// <value>The classes.</value>
    public string Classes { get; set; }

    /// <summary>
    /// Gets or sets the contents.
    /// </summary>
    /// <value>The contents.</value>
    public string Contents { get; set; }
}