namespace FileSystemVisitorPL
{
    public class FileSystemVisitor : IFileSystemVisitor
    {
        public event EventHandler SearchStarted;

        public event EventHandler SearchFinished;

        public event EventHandler<FileSystemEventArgs> FileFound;

        public event EventHandler<FileSystemEventArgs> DirectoryFound;

        public event EventHandler<FileSystemEventArgs> FilteredFileFound;

        public event EventHandler<FileSystemEventArgs> FilteredDirectoryFound;

        private readonly string rootFolder;
        private readonly Func<string, bool> searchFilter = _ => true;

        public FileSystemVisitor(string rootPath)
        {
            if (!Directory.Exists(rootPath))
            {
                ValidateRootDirectory();
            }

            rootFolder = rootPath;
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
            OnSearchStarted();

            foreach (var item in TraverseDirectory(rootFolder))
            {
                yield return item;
            }

            OnSearchFinished();
        }

        private IEnumerable<string> TraverseDirectory(string directory)
        {
            if (searchFilter(directory))
            {
                var (exclude, abort) = HandleEventsForFilteredItem(directory);

                if (!exclude)
                {
                    yield return directory;
                }
                if (abort)
                {
                    OnSearchFinished();
                    yield break;
                }
            }
            else
            {
                OnDirectoryFound(new FileSystemEventArgs(directory));
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
                    var (exclude, abort) = HandleEventsForFilteredItem(file);
                    
                    if (!exclude)
                    {
                        yield return file;
                    }

                    if (abort)
                    {
                        OnSearchFinished();

                        yield break;
                    }
                }
                else
                {
                    OnFileFound(new FileSystemEventArgs(file));
                }
            }
        }

        private (bool Exclude, bool Abort) HandleEventsForFilteredItem(string item)
        {
            var args = new FileSystemEventArgs(item);
            if (Path.HasExtension(item))
            {
                OnFilteredFileFound(args);
            }
            else
            {
                OnFilteredDirectoryFound(args);
            }
            return (args.ExcludeItem, args.AbortSearch);
        }

        private void ValidateRootDirectory()
        {
            if (!Directory.Exists(rootFolder))
            {
                throw new DirectoryNotFoundException("The specified root folder does not exist.");
            }
        }

        protected virtual void OnSearchStarted()
        {
            SearchStarted?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnSearchFinished()
        {
            SearchFinished?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnFileFound(FileSystemEventArgs e)
        {
            FileFound?.Invoke(this, e);
        }

        protected virtual void OnDirectoryFound(FileSystemEventArgs e)
        {
            DirectoryFound?.Invoke(this, e);
        }

        protected virtual void OnFilteredFileFound(FileSystemEventArgs e)
        {
            FilteredFileFound?.Invoke(this, e);
        }

        protected virtual void OnFilteredDirectoryFound(FileSystemEventArgs e)
        {
            FilteredDirectoryFound?.Invoke(this, e);
        }
    }
}