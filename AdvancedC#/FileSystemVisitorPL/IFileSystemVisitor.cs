namespace FileSystemVisitorPL;

public interface IFileSystemVisitor
{
    IEnumerable<string> GetFilesAndFolders();
}
