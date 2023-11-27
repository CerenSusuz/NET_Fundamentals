namespace FileSystemVisitorPL;

public class FileSystemEventArgs : EventArgs
{
    public bool AbortSearch { get; set; }

    public bool ExcludeItem { get; set; }

    public string Item { get; set; }

    public FileSystemEventArgs(string item)
    {
        Item = item;
    }
}