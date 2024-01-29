using LibraryApp.Documents;
using LibraryApp.Repositories;

namespace LibraryApp.Services;

internal class MagazineService(IRepository<Magazine> repository) : IService<Magazine>
{
    private readonly IRepository<Magazine> _repository = repository;

    public void Create(Magazine document)
    {
        if (_repository.Read(document.Title) != null)
        {
            Console.WriteLine($"The document ({document.Title}) already exists. Skipping creation.");

            return;
        }

        _repository.Create(document);
    }

    public IList<Magazine> GetAllDocuments()
    {
        return _repository.ReadAll();
    }

    public Magazine GetDocumentByTitle(string title)
    {
        return _repository.Read(title);
    }

    public IList<Magazine> GetMagazinesByYear(int year)
    {
        return _repository.ReadAll().Where(magazine => magazine.DatePublished.Year == year).ToList();
    }
}
