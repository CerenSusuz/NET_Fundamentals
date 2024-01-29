using System.Text.Json.Serialization;

namespace LibraryApp.Documents;

[method: JsonConstructor]
public class Book(
    string title,
    List<string> authors,
    DateTime datePublished,
    int numberOfPages,
    string publisher
    ) : BaseDocument(title, authors, datePublished)
{
    [JsonPropertyName("isbn")]
    public string ISBN { get; set; }

    [JsonPropertyName("numberOfPages")]
    public int NumberOfPages { get; set; } = numberOfPages;

    [JsonPropertyName("publisher")]
    public string Publisher { get; set; } = publisher;

    [JsonPropertyName("documentType")]
    public override DocumentType Type { get; set; }
}
