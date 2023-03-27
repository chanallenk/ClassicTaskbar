using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsLauncher.Contracts.Models;
public interface IRegistryAction
{
    Action<string, string, int>? SetRegistryValue
    {
        get;set;
    }

    void LoadRegistryValue(Func<string, string, object> tryLoadRegistrySettings);
}
