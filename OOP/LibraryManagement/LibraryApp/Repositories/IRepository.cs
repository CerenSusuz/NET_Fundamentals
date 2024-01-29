using LibraryApp.Documents;

namespace LibraryApp.Repositories;

public interface IRepository<T> where T : BaseDocument
{
    T Read(string title);

    IList<T> ReadAll();

    public void Create(T document);
}
