using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Win32;
using SettingsLauncher.Contracts.Models;
using SettingsLauncher.Helpers;

namespace SettingsLauncher.Models.LogicalControls;
public class Windows11Version22H2OrHigherLogicalControl : LogicalControlBase, ILogical
{
    public string Name => "IsWindows11Version22H2OrHigher";

    public bool Inverted
    {
        get;
        set;
    }

    public override bool CanBeEnabled()
    {
        bool returnVal = false;
        try
        {
            var displayVersion = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "DisplayVersion", "")?.ToString();
            var buildVersion = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentBuild", "")?.ToString()?.ToInt();

            returnVal = (buildVersion > 19045 || (displayVersion != null && Regex.IsMatch(displayVersion, "([2-9](([2-9][0-9a-zA-Z][2-9])|([3-9]..)))|([3-9]...)")));
        }
        catch (Exception ex) { }

        return Inverted ? !(returnVal) : returnVal;
    }
}
