namespace FileSystemVisitorTests;

public class FileSystemVisitorTests
{
    private readonly string _testDirectory;
    private readonly IFileExplorer _fileExplorer;
    private const int ExpectedFilesFoundCount = 6;
    private const int ExpectedFilteredFilesFoundCount = 3;

    public FileSystemVisitorTests()
    {
        var tempPath = Path.GetTempPath();
        _testDirectory = Path.Combine(tempPath, Guid.NewGuid().ToString());

        Directory.CreateDirectory(_testDirectory);
        Directory.CreateDirectory(Path.Combine(_testDirectory, "dir1"));
        Directory.CreateDirectory(Path.Combine(_testDirectory, "dir2"));

        File.WriteAllText(Path.Combine(_testDirectory, "dir1", "file1.txt"), "content");
        File.WriteAllText(Path.Combine(_testDirectory, "dir1", "file2.txt"), "content");
        File.WriteAllText(Path.Combine(_testDirectory, "dir2", "file3.txt"), "content");

        _fileExplorer = new FileExplorer();
    }

    [Fact]
    public void Test_GetFilesAndFolders_WithoutFilter()
    {
        // Arrange
        var visitor = new FileSystemVisitor(_testDirectory, _fileExplorer);

        // Act
        var result = visitor.GetFilesAndFolders().ToList();

        // Assert
        Assert.Equal(ExpectedFilesFoundCount, result.Count);
        Assert.Contains(Path.Combine(_testDirectory, "dir1"), result);
        Assert.Contains(Path.Combine(_testDirectory, "dir2"), result);
        Assert.Contains(Path.Combine(_testDirectory, "dir1", "file1.txt"), result);
        Assert.Contains(Path.Combine(_testDirectory, "dir1", "file2.txt"), result);
        Assert.Contains(Path.Combine(_testDirectory, "dir2", "file3.txt"), result);
    }

    [Fact]
    public void Test_GetFilesAndFolders_WithFilter()
    {
        // Arrange
        Func<string, bool> filter = path => path.EndsWith(".txt");
        var visitor = new FileSystemVisitor(_testDirectory, filter, _fileExplorer);

        // Act
        var result = visitor.GetFilesAndFolders().ToList();

        // Assert
        Assert.Equal(ExpectedFilteredFilesFoundCount, result.Count);
        Assert.Contains(Path.Combine(_testDirectory, "dir1", "file1.txt"), result);
        Assert.Contains(Path.Combine(_testDirectory, "dir1", "file2.txt"), result);
        Assert.Contains(Path.Combine(_testDirectory, "dir2", "file3.txt"), result);
    }
}