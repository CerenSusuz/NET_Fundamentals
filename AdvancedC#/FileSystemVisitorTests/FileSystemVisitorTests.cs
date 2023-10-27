using Xunit;

namespace FileSystemVisitorTests;

public class FileSystemVisitorTests
{
    private readonly string _testDirectory;

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
    }

    [Fact]
    public void Test_GetFilesAndFolders_WithoutFilter()
    {
        // Arrange
        var visitor = new FileSystemVisitorPL.FileSystemVisitor(_testDirectory);

        // Act
        var result = visitor.GetFilesAndFolders().ToList();

        // Assert
        Assert.Equal(6, result.Count);
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
        var visitor = new FileSystemVisitorPL.FileSystemVisitor(_testDirectory, filter);

        // Act
        var result = visitor.GetFilesAndFolders().ToList();

        // Assert
        Assert.Equal(3, result.Count);
        Assert.Contains(Path.Combine(_testDirectory, "dir1", "file1.txt"), result);
        Assert.Contains(Path.Combine(_testDirectory, "dir1", "file2.txt"), result);
        Assert.Contains(Path.Combine(_testDirectory, "dir2", "file3.txt"), result);
    }
}