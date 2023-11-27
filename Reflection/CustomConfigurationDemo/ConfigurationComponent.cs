using CustomConfigurationDemo.Attributes;

namespace CustomConfigurationDemo;

public class ConfigurationComponent : BaseConfigurationComponent
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