using System;
using System.Collections.Generic;
using CustomConfigurationDemo.Providers;

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

        if (!Providers.TryGetValue(providerType, out IConfigurationProvider existingProvider))
        {
            Providers.Add(providerType, provider);

            return provider;
        }

        return existingProvider;
    }
}
