using System.Text.Json.Serialization;

namespace LibraryApp.Documents
{
    [method: JsonConstructor]
    public class LocalizedBook(
        string title,
        List<string> authors,
        DateTime datePublished,
        int numberOfPages,
        string publisher,
        string localPublisher,
        string countryOfLocalization) : Book(title, authors, datePublished, numberOfPages, publisher)
    {
        [JsonPropertyName("localPublisher")]
        public string LocalPublisher { get; set; } = localPublisher;

        [JsonPropertyName("countryOfLocalization")]
        public string CountryOfLocalization { get; set; } = countryOfLocalization;

        [JsonPropertyName("documentType")]
        public override DocumentType Type { get; set; } = DocumentType.LocalizedBook;
    }
}
