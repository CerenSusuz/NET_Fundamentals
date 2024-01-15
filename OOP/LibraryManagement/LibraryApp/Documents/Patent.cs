using System.Text.Json.Serialization;

namespace LibraryApp.Documents;

[method: JsonConstructor]
public class Patent(
    string title,
    List<string> authors,
    DateTime datePublished,
    DateTime expirationDate) : Document(title, authors, datePublished)
{
    [JsonPropertyName("expirationDate")]
    public DateTime ExpirationDate { get; set; } = expirationDate;

    [JsonPropertyName("uniqueId")]
    public string UniqueId { get; set; }

    [JsonPropertyName("documentType")]
    public override DocumentType Type { get; set; } = DocumentType.Patent;
}
