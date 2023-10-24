using System;
using System.IO;
using System.Collections.Generic;

namespace FileSystemVisitorPL
{
    public class FileSystemVisitor
    {
        private readonly string rootFolder;

        public FileSystemVisitor(string rootPath)
        {
            if (!Directory.Exists(rootPath))
            {
                throw new DirectoryNotFoundException("The specified root folder does not exist.");
            }

            rootFolder = rootPath;
        }

        public IEnumerable<string> GetFilesAndFolders()
        {
            return TraverseDirectory(rootFolder);
        }

        private IEnumerable<string> TraverseDirectory(string directory)
        {
            yield return directory;

            foreach (string subdirectory in Directory.GetDirectories(directory))
            {
                foreach (string item in TraverseDirectory(subdirectory))
                {
                    yield return item;
                }
            }

            foreach (string file in Directory.GetFiles(directory))
            {
                yield return file;
            }
        }
    }
}