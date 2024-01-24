using LibraryApp.Documents;
using LibraryApp.Repositories;

namespace LibraryApp.Services;

public class BookService(IRepository<Book> repository) : IService<Book>
{
    private readonly IRepository<Book> _repository = repository;

    public void Create(Book document)
    {
        if (_repository.Read(document.Title) != null)
        {
            Console.WriteLine($"The document ({document.Title}) already exists. Skipping creation.");

            return;
        }

        _repository.Create(document);
    }

    public IList<Book> GetAllDocuments()
    {
        return _repository.ReadAll();
    }

    public Book GetDocumentByTitle(string title)
    {
        return _repository.Read(title);
    }

    public IList<Book> GetBooksByPublisher(string publisher)
    {
        return _repository.ReadAll().Where(book => book.Publisher == publisher).ToList();
    }
}
