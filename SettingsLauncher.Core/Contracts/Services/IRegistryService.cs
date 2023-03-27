using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsLauncher.Core.Contracts.Services;
public interface IRegistryService
{
    void SaveValue<T>(string path, string key, T value);

    T LoadValue<T>(string path, string key);
}
