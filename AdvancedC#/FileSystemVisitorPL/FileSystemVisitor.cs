namespace FileSystemVisitorPL
{
    public class FileSystemVisitor : IFileSystemVisitor
    {
        private readonly string rootFolder;
        private readonly Func<string, bool> searchFilter;

        public FileSystemVisitor(string rootPath)
        {
            if (!Directory.Exists(rootPath))
            {
                throw new DirectoryNotFoundException("The specified root folder does not exist.");
            }

            rootFolder = rootPath;
            filter = (path) => true; // Default filter accepts all items
        }

        public FileSystemVisitor(string rootPath, Func<string, bool> customFilter)
        {
            if (!Directory.Exists(rootPath))
            {
                throw new DirectoryNotFoundException("The specified root folder does not exist.");
            }

            rootFolder = rootPath;
            filter = customFilter;
        }

        public IEnumerable<string> GetFilesAndFolders()
        {
            return TraverseDirectory(rootFolder);
        }

        private IEnumerable<string> TraverseDirectory(string directory)
        {
            if (filter(directory))
            {
                yield return directory;
            }

            foreach (string subdirectory in Directory.GetDirectories(directory))
            {
                foreach (string item in TraverseDirectory(subdirectory))
                {
                    yield return item;
                }
            }

            foreach (string file in Directory.GetFiles(directory))
            {
                if (filter(file))
                {
                    yield return file;
                }
            }
        }
    }
}