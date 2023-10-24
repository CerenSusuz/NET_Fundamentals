using FileSystemVisitorPL;

Console.Write("Enter the root folder path: ");
string rootPath = Console.ReadLine();

Console.Write("Enter a filter (e.g., '.txt' or 'cs'): ");
string filter = Console.ReadLine();

Func<string, bool> customFilter = item =>
{
    if (string.IsNullOrWhiteSpace(filter))
    {
        return true;
    }
    string extension = Path.GetExtension(item);
    
    return !string.IsNullOrWhiteSpace(extension) && extension.Equals(filter, StringComparison.OrdinalIgnoreCase);
};

if (string.IsNullOrEmpty(rootPath))
{
    Console.WriteLine("Invalid root folder path.");
    
    return;
}

var fileSystemVisitor = new FileSystemVisitor(rootPath, customFilter);

foreach (string item in fileSystemVisitor.GetFilesAndFolders())
{
    Console.WriteLine(item);
}

Console.ReadKey();