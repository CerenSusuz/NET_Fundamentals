using FileSystemVisitorPL;

Console.Write("Enter the root folder path: ");
string rootPath = Console.ReadLine();

Console.Write("Enter a filter (e.g., '.txt' or 'cs'): ");
string searchFilter = Console.ReadLine();

Func<string, bool> customFilter = item =>
{
    if (string.IsNullOrWhiteSpace(searchFilter))
    {
        return true;
    }

    string extension = Path.GetExtension(item);
    
    return !string.IsNullOrWhiteSpace(extension) && extension.Equals(searchFilter, StringComparison.OrdinalIgnoreCase);
};

if (string.IsNullOrEmpty(rootPath))
{
    Console.WriteLine("Invalid root folder path.");
    
    return;
}

var fileSystemVisitor = new FileSystemVisitor(rootPath, customFilter);

fileSystemVisitor.SearchStarted += (sender, e) => Console.WriteLine("---Search started.");
fileSystemVisitor.SearchFinished += (sender, e) => Console.WriteLine("---Search finished.");
fileSystemVisitor.FileFound += (sender, e) => Console.WriteLine($"---Found file: {e.Item}");
fileSystemVisitor.DirectoryFound += (sender, e) => Console.WriteLine($"---Found directory: {e.Item}");
fileSystemVisitor.FilteredFileFound += (sender, e) => Console.WriteLine($"---Filtered file found: {e.Item}");
fileSystemVisitor.FilteredDirectoryFound += (sender, e) => Console.WriteLine($"---Filtered directory found: {e.Item}");

var results = fileSystemVisitor.GetFilesAndFolders().ToList();

Console.WriteLine("\nFinal results:");
foreach (string item in results)
{
    Console.WriteLine(item);
}

Console.ReadKey();