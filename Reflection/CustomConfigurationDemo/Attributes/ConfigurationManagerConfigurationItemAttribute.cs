using CustomConfigurationDemo.Providers;

namespace CustomConfigurationDemo.Attributes;

public class ConfigurationManagerConfigurationItemAttribute : BaseConfigurationItemAttribute
{
    public ConfigurationManagerConfigurationItemAttribute(string settingName)
        : base(settingName, new ConfigurationManagerConfigurationProvider()) { }
}
