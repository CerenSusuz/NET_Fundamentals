using LibraryApp.Documents;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Text.Json;
using LibraryApp.Services;

namespace LibraryApp.Repositories;

public class JsonFileDocumentRepository<T>(DocumentCache cache) : IRepository<T> where T : Document
{
    private const string DocType = "documentType";
    private readonly DocumentCache _cache = cache;
    private readonly string _baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
    private readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true, IncludeFields = true };

    public T Read(string title)
    {
        var document = _cache.GetDocument<T>(title);

        if (document == null)
        {
            var file = new DirectoryInfo(_baseDirectory).GetFiles().FirstOrDefault(f => f.Name.Contains(title));

            if (file != null)
            {
                var json = File.ReadAllText(file.FullName);
                var jObject = JObject.Parse(json);
                var docType = jObject[DocType].ToObject<DocumentType>();

                document = Deserialize(jObject, docType);
                _cache.SetDocument(document);
            }
        }

        return document;
    }

    public IList<T> ReadAll()
    {
        var directoryInfo = new DirectoryInfo(_baseDirectory);
        var files = directoryInfo.GetFiles("type_*_*_*.json");
        var documents = new List<T>();

        foreach (var file in files)
        {
            var title = GetTitle(file.Name);
            var document = Read(title);

            if (document != null)
            {
                documents.Add(document);
            }
        }

        return documents;
    }

    public void Create(T document)
    {
        if (Read(document.Title) != null)
        {
            throw new Exception("A document with this title and type already exists.");
        }

        var jsonString = JsonSerializer.Serialize(document, _jsonOptions);
        var directory = Path.Combine(_baseDirectory, GetFileName(document));

        File.WriteAllText(directory, jsonString);
    }

    private static string GetTitle(string fileName)
    {
        var match = Regex.Match(fileName, @"^type_.*_(.*)_.*\.json$");

        if (match.Success)
        {
            return match.Groups[1].Value;
        }

        throw new Exception("Couldn't parse the file name.");
    }

    private static string GetFileName(T document)
    {
        return $"type_{document.Type}_{document.Title}_{document.Id}.json";
    }

    private static T Deserialize(JObject jObject, DocumentType docType)
    {
        return docType switch
        {
            DocumentType.LocalizedBook => jObject.ToObject<LocalizedBook>() as T,
            DocumentType.Book => jObject.ToObject<Book>() as T,
            DocumentType.Patent => jObject.ToObject<Patent>() as T,
            DocumentType.Magazine => jObject.ToObject<Magazine>() as T,
            _ => throw new Exception("Unhandled document type detected.")
        };
    }
}