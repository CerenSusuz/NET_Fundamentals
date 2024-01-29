using System.Text.Json.Serialization;

namespace LibraryApp.Documents;

public abstract class BaseDocument
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("authors")]
    public List<string> Authors { get; set; } = new List<string>();

    [JsonPropertyName("datePublished")]
    public DateTime DatePublished { get; set; }

    [JsonPropertyName("documentType")]
    public virtual DocumentType Type { get; set; }

    protected BaseDocument(string title)
    {
        Title = title;
    }

    protected BaseDocument(string title, List<string> authors, DateTime datePublished)
    {
        Id = Guid.NewGuid();
        Title = title;
        Authors = authors;
        DatePublished = datePublished;
    }
}
