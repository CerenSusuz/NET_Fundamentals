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

    public Type ProviderType { get; set; }

    protected BaseConfigurationItemAttribute(string settingName, Type providerType)
    {
        SettingName = settingName;
        ProviderType = providerType;
    }
}
