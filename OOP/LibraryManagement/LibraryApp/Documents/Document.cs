using System.Text.Json.Serialization;

namespace LibraryApp.Documents;

[method: JsonConstructor]
public abstract class Document(
    string title,
    List<string> authors,
    DateTime datePublished)
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [JsonPropertyName("title")]
    public string Title { get; set; } = title;

    [JsonPropertyName("authors")]
    public List<string> Authors { get; set; } = authors ?? [];

    [JsonPropertyName("datePublished")]
    public DateTime DatePublished { get; set; } = DateTime.Now;

    [JsonPropertyName("documentType")]
    public virtual DocumentType Type { get; set; }
}
