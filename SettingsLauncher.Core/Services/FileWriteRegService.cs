using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Win32;
using SettingsLauncher.Core.Contracts.Services;

namespace SettingsLauncher.Core.Services;
public class FileWriteRegService : IRegistryService
{
    private readonly IFileService _fileService;
    public FileWriteRegService(IFileService fileService)
    {
        this._fileService = fileService;
    }

    public void SaveValue<T>(string path, string key, T value)
    {
        Trace.WriteLine($"Saving value {path}//{key} : {value}");


        var registryKey = Registry.CurrentUser.OpenSubKey(Regex.Replace(path, "HKEY_CURRENT_USER\\\\", ""), true);

        if (registryKey != null)
        {
            //registryKey.SetValue(key, value.ToString()); //sets 'someData' in 'someValue' 

            registryKey.Close();
        }

    }

    public T LoadValue<T>(string path, string key)
    {
        var registryKey = Registry.CurrentUser.OpenSubKey(Regex.Replace(path, "HKEY_CURRENT_USER\\\\", ""), true);

        if (registryKey != null)
        {
            var value = registryKey.GetValue(key);

            registryKey.Close();

            Trace.WriteLine($"Loading value {path} : {value}");

            return (T)value;
        }

        return default(T);
    }
}
