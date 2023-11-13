using CustomConfigurationDemo.Attributes;

namespace CustomConfigurationDemo;

public class ConfigurationComponent : ConfigurationComponentBase
{
    [FileConfigurationItem("IntProperty")]
    public int IntProperty
    {
        get => GetPropertyValue<int>();
        set => SetPropertyValue(value);
    }

    [ConfigurationManagerConfigurationItem("StringProperty")]
    public string StringProperty
    {
        get => GetPropertyValue<string>();
        set => SetPropertyValue(value);
    }
}