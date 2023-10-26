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
                ValidateRootDirectory();
            }

            rootFolder = rootPath;
            searchFilter = (path) => true;
        }

        public FileSystemVisitor(string rootPath, Func<string, bool> customFilter)
        {
            if (!Directory.Exists(rootPath))
            {
                ValidateRootDirectory();
            }

            rootFolder = rootPath;
            searchFilter = customFilter;
        }

        public IEnumerable<string> GetFilesAndFolders()
        {
            return TraverseDirectory(rootFolder);
        }

        private IEnumerable<string> TraverseDirectory(string directory)
        {
            if (searchFilter(directory))
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
                if (searchFilter(file))
                {
                    yield return file;
                }
            }
        }

        private void ValidateRootDirectory()
        {
            if (!Directory.Exists(rootFolder))
            {
                throw new DirectoryNotFoundException("The specified root folder does not exist.");
            }
        }

    }
}