namespace FileSystemVisitorPL;

public interface IFileExplorer
{
    IEnumerable<string> GetDirectories(string path);

    IEnumerable<string> GetFiles(string path);

    string GetExtension(string path);

    bool Exists(string path);
}