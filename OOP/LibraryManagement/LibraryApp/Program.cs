using LibraryApp.Documents;
using LibraryApp.Repositories;
using LibraryApp.Services;

namespace LibraryApp;


public static class Program
{
    private static readonly string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

    public static void Main()
    {
        var cacheExpiryPerType = new Dictionary<DocumentType, TimeSpan>
        {
            {DocumentType.Book, TimeSpan.FromMinutes(30)},
            {DocumentType.Patent, TimeSpan.FromHours(2)},
            {DocumentType.LocalizedBook, TimeSpan.FromDays(1)},
            {DocumentType.Magazine, TimeSpan.FromDays(365 * 100)}
        };

        var serviceBooks = new BookService(new JsonFileDocumentRepository<Book>(new DocumentCache(cacheExpiryPerType), baseDirectory));
        var servicePatents = new PatentService(new JsonFileDocumentRepository<Patent>(new DocumentCache(cacheExpiryPerType), baseDirectory));
        var serviceLocalizedBooks = new LocalizedBookService(new JsonFileDocumentRepository<LocalizedBook>(new DocumentCache(cacheExpiryPerType), baseDirectory));
        var serviceMagazines = new MagazineService(new JsonFileDocumentRepository<Magazine>(new DocumentCache(cacheExpiryPerType), baseDirectory));

        foreach (var document in CreateInitialDocuments())
        {
            switch (document)
            {
                case LocalizedBook l:
                    serviceLocalizedBooks.Create(l);
                    break;
                case Book book:
                    serviceBooks.Create(book);
                    break;
                case Patent patent:
                    servicePatents.Create(patent);
                    break;
                case Magazine magazine:
                    serviceMagazines.Create(magazine);
                    break;
            }
        }

        PrintDocument(serviceBooks.GetDocumentByTitle("Ulysses"));
        Console.WriteLine("--");
        PrintDocuments(serviceMagazines.GetAllDocuments());
        Console.WriteLine("--");
        PrintDocuments(servicePatents.GetAllDocuments());
        Console.WriteLine("--");
        PrintDocuments(serviceLocalizedBooks.GetAllDocuments());
    }

    private static IList<BaseDocument> CreateInitialDocuments() => new List<BaseDocument>
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

    private static void PrintDocument(BaseDocument document)
    {
        if (document != null)
        {
            Console.WriteLine($"Document Type and Title: {document.Type}/{document.Title}");
        }
    }

    private static void PrintDocuments<T>(IList<T> documents) where T : BaseDocument
    {
        foreach (var doc in documents)
        {
            PrintDocument(doc);
        }
    }
}