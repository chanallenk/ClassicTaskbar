using System.Collections.ObjectModel;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml;
using SettingsLauncher.Contracts.Models;
using SettingsLauncher.Contracts.Services;
using SettingsLauncher.Contracts.ViewModels;
using SettingsLauncher.Core.Contracts.Services;
using SettingsLauncher.Core.Models;
using SettingsLauncher.Helpers;
using Windows.ApplicationModel;

namespace SettingsLauncher.ViewModels.Core;

public abstract class CorePropertiesViewModelBase : ObservableRecipient, IControlLogicViewModel
{
    protected readonly IRegistryService _registryService;
    protected readonly IControlLogicService _controlLogicService;

    public abstract ObservableCollection<IControl> Controls
    {
        get;
    }

    public CorePropertiesViewModelBase(IRegistryService registryService, IControlLogicService controlLogicService)
    {
        _registryService = registryService;
        _controlLogicService = controlLogicService;
        WeakReferenceMessenger.Default.Register<string>(this, (r, m) => ProcessMessage(r, m));
        InitializeControls();
    }

    public void InitializeControls()
    {
        foreach (var c in this.Controls)
        {
            if (c is IRegistryAction)
            {
                IRegistryAction rc = (IRegistryAction)c;
                rc.SetRegistryValue = SaveRegistrySettings;
                rc.LoadRegistryValue(TryLoadRegistrySettings);
            }
        }
    }

    public object TryLoadRegistrySettings(string fullPath, string key)
    {
        //Try to load registry settings, otherwise fall back to the default value
        return _registryService.LoadValue<object>(fullPath, key);
    }
    public void SaveRegistrySettings(string fullPath, string key, int dword)
    {
        _registryService.SaveValue<int>(fullPath, key, dword);
    }

    public void RefreshControls()
    {
        OnPropertyChanged("Controls");
    }

    public void ProcessMessage(object r, string input)
    {
        switch (input)
        {
            case "RefreshControls()":
                RefreshControls();
                break;
            default:
                break;
        }

    }

}
