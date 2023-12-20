namespace CustomConfigurationDemo.Attributes;

public class FileConfigurationItemAttribute : BaseConfigurationItemAttribute
{
    public FileConfigurationItemAttribute(string settingName)
        : base(settingName, new FileConfigurationProvider()) { }
}
