using CustomConfigurationDemo.Providers;

namespace CustomConfigurationDemo.Attributes;

public class ConfigurationManagerConfigurationItemAttribute : ConfigurationItemAttribute
{
    public ConfigurationManagerConfigurationItemAttribute(string settingName)
        : base(settingName, typeof(ConfigurationManagerConfigurationProvider)) { }
}
