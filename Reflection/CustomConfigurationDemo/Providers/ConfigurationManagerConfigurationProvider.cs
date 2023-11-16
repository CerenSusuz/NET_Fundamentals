using Newtonsoft.Json;
using Serializer = Newtonsoft.Json.JsonSerializer;

namespace CustomConfigurationDemo.Providers;

public class ConfigurationManagerConfigurationProvider : IConfigurationProvider
{
    private readonly string _appSettingsPath;
    private Dictionary<string, string> _appSettings;
    private Serializer _serializer;

    public ConfigurationManagerConfigurationProvider()
        : this(Path.Combine(Environment.CurrentDirectory, "appsettings.json"))
    {
    }

    public ConfigurationManagerConfigurationProvider(string appSettingsPath)
    {
        _appSettingsPath = appSettingsPath;
        _serializer = new Serializer();
        LoadSettingsFromFile();
    }

    private void LoadSettingsFromFile()
    {
        using (var streamReader = new StreamReader(_appSettingsPath))
        using (var reader = new JsonTextReader(streamReader))
        {
            _appSettings = _serializer.Deserialize<Dictionary<string, string>>(reader);
        }
    }

    public void SetValue(string key, object value)
    {
        _appSettings[key] = value.ToString();

        try
        {
            using (var streamWriter = new StreamWriter(_appSettingsPath))
            using (var writer = new JsonTextWriter(streamWriter))
            {
                _serializer.Serialize(writer, _appSettings);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public object GetValue(string key, Type valueType)
    {
        return Convert.ChangeType(_appSettings[key], valueType);
    }
}