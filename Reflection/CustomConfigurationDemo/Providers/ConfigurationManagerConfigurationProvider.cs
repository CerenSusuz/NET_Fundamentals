using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Serializer = Newtonsoft.Json.JsonSerializer;

namespace CustomConfigurationDemo.Providers;

public class ConfigurationManagerConfigurationProvider : IConfigurationProvider
{
    private readonly string _appSettingsPath = Path.Combine(Environment.CurrentDirectory, "appsettings.json");
    private Dictionary<string, string> _appSettings;
    private Serializer _serializer;

    public ConfigurationManagerConfigurationProvider()
    {
        _serializer = new Serializer();
        LoadSettingsFromFile();
    }

    private void LoadSettingsFromFile()
    {
        using (var sr = new StreamReader(_appSettingsPath))
        using (var reader = new JsonTextReader(sr))
        {
            _appSettings = _serializer.Deserialize<Dictionary<string, string>>(reader);
        }
    }

    public void SetValue(string key, object value)
    {
        _appSettings[key] = value.ToString();

        using (var sw = new StreamWriter(_appSettingsPath))
        using (var writer = new JsonTextWriter(sw))
        {
            _serializer.Serialize(writer, _appSettings);
        }
    }

    public object GetValue(string key, Type valueType)
    {
        return Convert.ChangeType(_appSettings[key], valueType);
    }
}

