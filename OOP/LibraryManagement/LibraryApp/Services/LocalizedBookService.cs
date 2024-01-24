using LibraryApp.Documents;
using LibraryApp.Repositories;

namespace LibraryApp.Services;

public class LocalizedBookService(IRepository<LocalizedBook> repository) : IService<LocalizedBook>
{
    private readonly IRepository<LocalizedBook> _repository = repository;

    public LocalizedBook GetDocumentByTitle(string title)
    {
        return _repository.Read(title);
    }

    public IList<LocalizedBook> GetAllDocuments()
    {
        return _repository.ReadAll();
    }

    public void Create(LocalizedBook document)
    {
        if (_repository.Read(document.Title) != null)
        {
            Console.WriteLine($"The document ({document.Title}) already exists. Skipping creation.");

            return;
        }

        _repository.Create(document);
    }

    public IList<LocalizedBook> GetBooksByLocalization(string country)
    {
        return _repository.ReadAll().Where(book => book.CountryOfLocalization == country).ToList();
    }
}
