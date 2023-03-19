using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using SettingsLauncher.Contracts.Models;
using SettingsLauncher.Helpers;

namespace SettingsLauncher.Models.LogicalControls;
public class OldTaskBarLogicalControl : LogicalControlBase, ILogical
{
    public string Name => "IsOldTaskbar";

    public bool Inverted
    {
        get; 
        set; 
    }

    public override bool CanBeEnabled()
    {
#if DEBUG
        return true;
#endif
        bool returnVal = false;
        try
        {
            var oldTaskbar = Registry.GetValue(@"HKEY_CURRENT_USER\Software\ExplorerPatcher", "OldTaskbar", "")?.ToString()?.ToInt();

            returnVal = (oldTaskbar != null && oldTaskbar > 0);
        }
        catch (Exception ex) { }

        return Inverted ? !(returnVal) : returnVal;
    }
}
