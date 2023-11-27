using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemVisitorPL;

public class FileExplorer : IFileExplorer
{
    public IEnumerable<string> GetDirectories(string path)
    {
        return Directory.GetDirectories(path);
    }

    public IEnumerable<string> GetFiles(string path)
    {
        return Directory.GetFiles(path);
    }

    public string GetExtension(string path)
    {
        return Path.GetExtension(path);
    }

    public bool Exists(string path)
    {
        return Directory.Exists(path);
    }
}
