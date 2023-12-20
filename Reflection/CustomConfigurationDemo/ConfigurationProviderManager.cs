namespace CustomConfigurationDemo;

public static class ConfigurationProviderManager
{
    private static readonly Dictionary<Type, IConfigurationProvider> Providers = new();

    public static IConfigurationProvider GetProvider(IConfigurationProvider provider)
    {
        if (provider == null)
        {
            throw new ArgumentNullException(nameof(provider));
        }

        var providerType = provider.GetType();

        if (!Providers.TryGetValue(key: providerType, value: out IConfigurationProvider existingProvider))
        {
            Providers.Add(key: providerType, value: provider);

            return provider;
        }

        return existingProvider;
    }
}
