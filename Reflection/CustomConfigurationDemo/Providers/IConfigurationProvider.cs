using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomConfigurationDemo.Providers;

public interface IConfigurationProvider
{
    void SetValue(string key, object value);

    object GetValue(string key, Type valueType);
}
