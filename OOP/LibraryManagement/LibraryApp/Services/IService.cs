using LibraryApp.Documents;

namespace LibraryApp.Services;

public interface IService<T> where T : Document
{
    T GetDocumentByTitle(string title);

    IList<T> GetAllDocuments();

    void Create(T document);
}
