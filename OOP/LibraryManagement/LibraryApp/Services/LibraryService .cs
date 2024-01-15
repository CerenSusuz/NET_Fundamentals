using LibraryApp.Documents;
using LibraryApp.Repositories;

namespace LibraryApp.Services
{
    public class LibraryService<T>(IRepository<T> repository) : IService<T> where T : Document
    {
        private readonly IRepository<T> _repository = repository;

        public T GetDocumentByTitle(string title)
        {
            return _repository.Read(title);
        }

        public IList<T> GetAllDocuments()
        {
            return _repository.ReadAll();
        }

        public void Create(T document)
        {
            _repository.Create(document);
        }
    }
}
