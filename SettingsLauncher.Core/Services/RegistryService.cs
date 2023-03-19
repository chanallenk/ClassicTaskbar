using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Win32;
using SettingsLauncher.Core.Contracts.Services;

namespace SettingsLauncher.Core.Services;
public class RegistryService : IRegistryService
{
    public void SaveValue<T>(string path, string key, T value)
    {
        try
        {
            if (!String.IsNullOrWhiteSpace(path))
            {
                var registryKey = Registry.CurrentUser.OpenSubKey(Regex.Replace(path, "HKEY_CURRENT_USER\\\\", ""), true);

                if (registryKey != null)
                {
                    registryKey.SetValue(key, value, RegistryValueKind.DWord); //sets 'someData' in 'someValue'  //todo what about hex values?

                    registryKey.Close();
                }
            }
            else
            {
                Trace.WriteLine($"Unsupported Path, Key: {key}");
            }
        }
        catch (Exception exc)
        {
        }
    }

    public T LoadValue<T>(string path, string key)
    {
        try
        {
            var registryKey = Registry.CurrentUser.OpenSubKey(Regex.Replace(path, "HKEY_CURRENT_USER\\\\", ""), true);

            if (registryKey != null)
            {
                var value = registryKey.GetValue(key);

                registryKey.Close();

                return (T)value;
            }
        }
        catch (Exception exc)
        {
        }
        return default(T);
    }

    public void LoadRegistry(string contents)
    {
        try
        {
            Process regeditProcess = Process.Start("regedit.exe", "/s key.reg");
            regeditProcess.WaitForExit();
        }
        catch (Exception exc)
        {
        }

    }
}
