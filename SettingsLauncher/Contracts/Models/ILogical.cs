using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsLauncher.Contracts.Models;
public interface ILogical
{
    public string Name
    {
        get;
    }

    public bool IsEnabled
    {
        get; 
    }

    public bool Inverted
    {
        get;
        set;
    }

    bool CanBeEnabled();
}
