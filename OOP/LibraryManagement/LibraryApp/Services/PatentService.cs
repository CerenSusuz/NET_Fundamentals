using LibraryApp.Documents;
using LibraryApp.Repositories;

namespace LibraryApp.Services;

public class PatentService(IRepository<Patent> repository) : IService<Patent>
{
    private readonly IRepository<Patent> _repository = repository;

    public void Create(Patent document)
    {
        if (_repository.Read(document.Title) != null)
        {
            Console.WriteLine($"The document ({document.Title}) already exists. Skipping creation.");

            return;
        }

        _repository.Create(document);
    }

    public IList<Patent> GetAllDocuments()
    {
        return _repository.ReadAll();
    }

    public Patent GetDocumentByTitle(string title)
    {
        return _repository.Read(title);
    }

    public IList<Patent> GetPatentsByValidity(DateTime referenceDate)
    {
        return _repository.ReadAll().Where(patent => patent.ExpirationDate > referenceDate).ToList();
    }
}