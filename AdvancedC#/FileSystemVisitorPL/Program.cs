using FileSystemVisitorPL;

FileSystemVisitor fileSystemVisitor = new("C:\\Users\\Ceren_Susuz\\Documents\\Work_Hard\\NET");

foreach (string item in fileSystemVisitor.GetFilesAndFolders())
{
    Console.WriteLine(item);
}