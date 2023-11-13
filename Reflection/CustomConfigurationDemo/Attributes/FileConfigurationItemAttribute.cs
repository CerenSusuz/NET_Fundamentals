using CustomConfigurationDemo.Providers;

namespace CustomConfigurationDemo.Attributes;

public class FileConfigurationItemAttribute : ConfigurationItemAttribute
{
    public FileConfigurationItemAttribute(string settingName)
        : base(settingName, typeof(FileConfigurationProvider)) { }
}
