using System.Text.Json.Serialization;

namespace LibraryApp.Documents;

[method: JsonConstructor]
public class Magazine(
    string title,
    string publisher,
    int releaseNumber,
    DateTime datePublished) : Document(title)
{
    [JsonPropertyName("publisher")]
    public string Publisher { get; set; } = publisher;

    [JsonPropertyName("releaseNumber")]
    public int ReleaseNumber { get; set; } = releaseNumber;

    [JsonPropertyName("documentType")]
    public override DocumentType Type { get; set; } = DocumentType.Magazine;
}
