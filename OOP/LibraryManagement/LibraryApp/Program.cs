using LibraryApp.Documents;
using LibraryApp.Repositories;
using LibraryApp.Services;

namespace LibraryApp;


public static class Program
{
    public static void Main()
    {
        var repository = new JsonFileDocumentRepository<Document>();
        var service = new LibraryService<Document>(repository);

        foreach (var document in CreateInitialDocuments())
        {
            service.Create(document);
        }

        PrintDocument(service.GetDocumentByTitle("Don Quijote"));
        Console.WriteLine("--");
        PrintDocuments(service.GetDocumentsByType(DocumentType.Magazine));
        Console.WriteLine("--");
        PrintDocuments(service.GetAllDocuments());
    }

    private static IList<Document> CreateInitialDocuments()
    {
        return new List<Document>
        {
            new Book(
                title: "Ulysses",
                authors: ["James Joyce"],
                datePublished: DateTime.Parse("1922-02-02"),
                numberOfPages: 730,
                publisher: "Sylvia Beach")
            { Type = DocumentType.Book },
            new LocalizedBook(
                title: "Don Quijote",
                authors: [ "Miguel de Cervantes" ],
                datePublished: DateTime.Parse("1615-01-01"),
                numberOfPages: 863,
                publisher: "Francisco de Robles",
                originalPublisher : "Francisco de Robles",
                countryOfLocalization: "Spain",
                localPublisher: "Planeta")
            { Type = DocumentType.LocalizedBook },
            new Patent(
                title: "Telephone",
                authors: ["Alexander Graham Bell" ],
                datePublished: DateTime.Parse("1876-03-07"),
                expirationDate: DateTime.Now.AddYears(20))
            { Type = DocumentType.Patent },
            new Magazine(
                title : "Science News",
                publisher : "Society for Science & the Public",
                releaseNumber : 3225,
                datePublished : DateTime.Parse("2021-09-29"))
            { Type = DocumentType.Magazine }

        };
    }

    private static void PrintDocument(Document document)
    {
        if (document != null)
        {
            Console.WriteLine($"Document Type and Title: {document.Type}/{document.Title}");
        }
    }

    private static void PrintDocuments(IList<Document> documents)
    {
        foreach (var doc in documents)
        {
            PrintDocument(doc);
        }
    }
}