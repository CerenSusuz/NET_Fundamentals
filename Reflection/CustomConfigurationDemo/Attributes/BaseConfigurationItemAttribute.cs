using CustomConfigurationDemo.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomConfigurationDemo.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public abstract class BaseConfigurationItemAttribute : Attribute
{
    public string SettingName { get; set; }

    public IConfigurationProvider ProviderType { get; set; }

    protected BaseConfigurationItemAttribute(string settingName, IConfigurationProvider providerType)
    {
        SettingName = settingName;
        ProviderType = providerType;
    }
}
