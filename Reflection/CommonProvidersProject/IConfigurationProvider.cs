namespace CommonProvidersProject;

public interface IConfigurationProvider
{
    void SetValue(string key, object value);

    object GetValue(string key, Type valueType);
}
