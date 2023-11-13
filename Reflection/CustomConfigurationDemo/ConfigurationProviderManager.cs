using System;
using System.Collections.Generic;
using CustomConfigurationDemo.Providers;

namespace CustomConfigurationDemo;

public static class ConfigurationProviderManager
{
    private static readonly Dictionary<Type, IConfigurationProvider> Providers = new();

    public static IConfigurationProvider GetProvider(Type providerType)
    {
        if (providerType == null)
        {
            throw new ArgumentNullException(nameof(providerType));
        }

        if (!Providers.TryGetValue(providerType, out IConfigurationProvider provider))
        {
            provider = Activator.CreateInstance(providerType) as IConfigurationProvider;
            Providers.Add(providerType, provider);
        }

        return provider;
    }
}
