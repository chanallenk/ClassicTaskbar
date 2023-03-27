using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using SettingsLauncher.Contracts.Models;

namespace SettingsLauncher.Models;
public class ControlBase : ObservableRecipient
{
    public string Label
    {
        get => _label;
        set => SetProperty(ref _label, value);
    }
    private string _label;

    public string RegistryPath
    {
        get => _registryPath;
        set => SetProperty(ref _registryPath, value);
    }
    private string _registryPath;

    public string RegistryKey
    {
        get => _registryKey;
        set => SetProperty(ref _registryKey, value);
    }
    private string _registryKey;

    public string RegistryFullKeyPath => this._registryPath + "\\" + this._registryKey;

    public ObservableCollection<ILogical> LogicalIsVisibles
    {
        get; set;
    } = new();

    public Action<string, string, int>? SetRegistryValue
    {
        get => _setRegistryValue;
        set => _setRegistryValue=value;
    }
    Action<string, string, int>? _setRegistryValue;
}
