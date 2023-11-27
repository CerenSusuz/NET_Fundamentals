using CommonProvidersProject;

namespace FileConfigurationProviderProject;

public class FileConfigurationProvider : IConfigurationProvider
{
    private readonly string _filePath;
    private Dictionary<string, string> _data = new Dictionary<string, string>();

    public FileConfigurationProvider() : this("config.txt") { }

    public FileConfigurationProvider(string filePath)
    {
        _filePath = filePath;
        Load();
    }

    private void Load()
    {
        _data = File.ReadAllLines(_filePath)
            .Select(line => line.Split('='))
            .ToDictionary(parts => parts[0], parts => parts[1]);
    }

    public void SetValue(string key, object value)
    {
        _data[key] = value.ToString();
        File.WriteAllLines(_filePath, _data.Select(kvp => $"{kvp.Key}={kvp.Value}"));
    }

    public object GetValue(string key, Type valueType)
    {
        return Convert.ChangeType(_data[key], valueType);
    }
}
