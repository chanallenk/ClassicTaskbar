using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml.Navigation;

using SettingsLauncher.Contracts.Services;
using SettingsLauncher.Services;
using SettingsLauncher.Views;

namespace SettingsLauncher.ViewModels;

public class ShellViewModel : ObservableRecipient
{
    private readonly IExplorerService _explorerService;
    private bool _isBackEnabled;
    private object? _selected;

    public INavigationService NavigationService
    {
        get;
    }

    public INavigationViewService NavigationViewService
    {
        get;
    }
    public ICommand CmdRestartExplorer
    {
        get;
    }
    public bool IsBackEnabled
    {
        get => _isBackEnabled;
        set => SetProperty(ref _isBackEnabled, value);
    }

    public object? Selected
    {
        get => _selected;
        set => SetProperty(ref _selected, value);
    }

    public ShellViewModel(INavigationService navigationService, INavigationViewService navigationViewService, IExplorerService explorerService)
    {
        NavigationService = navigationService;
        NavigationService.Navigated += OnNavigated;
        NavigationViewService = navigationViewService;
        _explorerService = explorerService;
        CmdRestartExplorer = new RelayCommand(RestartExplorer);
    }

    private void OnNavigated(object sender, NavigationEventArgs e)
    {
        IsBackEnabled = NavigationService.CanGoBack;

        if (e.SourcePageType == typeof(SettingsPage))
        {
            Selected = NavigationViewService.SettingsItem;
            return;
        }

        var selectedItem = NavigationViewService.GetSelectedItem(e.SourcePageType);
        if (selectedItem != null)
        {
            Selected = selectedItem;
        }
    }

    public void RestartExplorer()
    {
        try
        {
            _explorerService.Restart();
            WeakReferenceMessenger.Default.Send("RefreshControls()");

        }
        catch (Exception ex) { }
    }
}
