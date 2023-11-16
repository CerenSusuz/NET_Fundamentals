using System.Reflection;
using System.Runtime.CompilerServices;
using CustomConfigurationDemo.Attributes;
using CustomConfigurationDemo.Providers;

namespace CustomConfigurationDemo;

public abstract class BaseConfigurationComponent
{
    protected TValue? GetPropertyValue<TValue>([CallerMemberName] string propertyName = null)
    {
        var attribute = GetAttribute( propertyName);

        if (attribute != null)
        {
            var provider = GetProviderByAttribute(attribute);

            return (TValue)provider.GetValue(key: attribute.SettingName, valueType: typeof(TValue));
        }

        return default;
    }

    protected void SetPropertyValue<TValue>(TValue value, [CallerMemberName] string propertyName = null)
    {
        var attribute = GetAttribute(propertyName);

        if (attribute != null)
        {
            var provider = GetProviderByAttribute(attribute);
            provider.SetValue(key: attribute.SettingName, value: value);
        }
    }

    private BaseConfigurationItemAttribute? GetAttribute(string propertyName)
    {
        var propertyInfo = GetType().GetProperty(propertyName);

        return propertyInfo.GetCustomAttributes(typeof(BaseConfigurationItemAttribute), inherit: false)
            .FirstOrDefault() as BaseConfigurationItemAttribute;
    }

    private IConfigurationProvider GetProviderByAttribute(BaseConfigurationItemAttribute attribute)
    {
        return ConfigurationProviderManager.GetProvider(provider: attribute.ProviderType);
    }

    public void LoadSettings()
    {
        var propertyInfos = GetType().GetProperties()
            .Where(property => property.GetCustomAttributes(attributeType: typeof(BaseConfigurationItemAttribute), inherit: false).Any());

        foreach (var propertyInfo in propertyInfos)
        {
            var attribute = propertyInfo.GetCustomAttributes(attributeType: typeof(BaseConfigurationItemAttribute), inherit: false)
                .FirstOrDefault() as BaseConfigurationItemAttribute;
            var provider = ConfigurationProviderManager.GetProvider(provider: attribute.ProviderType);

            propertyInfo.SetValue(obj: this, value: provider.GetValue(key: attribute.SettingName, valueType: propertyInfo.PropertyType));
        }
    }

    public void SaveSettings()
    {
        var propertyInfos = GetType().GetProperties()   
            .Where(property => property.GetCustomAttributes(attributeType: typeof(BaseConfigurationItemAttribute), inherit: false).Any());

        foreach (var propertyInfo in propertyInfos)
        {
            var attribute = propertyInfo.GetCustomAttributes(attributeType: typeof(BaseConfigurationItemAttribute), inherit: false)
                .FirstOrDefault() as BaseConfigurationItemAttribute;
            var provider = ConfigurationProviderManager.GetProvider(provider: attribute.ProviderType);

            provider.SetValue(key: attribute.SettingName, value: propertyInfo.GetValue(obj: this));
        }
    }
}