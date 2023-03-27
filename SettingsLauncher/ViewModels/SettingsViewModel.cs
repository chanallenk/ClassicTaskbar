using System.Reflection;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.UI.Xaml;

using SettingsLauncher.Contracts.Services;
using SettingsLauncher.Helpers;
using Windows.ApplicationModel;

namespace SettingsLauncher.ViewModels;

public class SettingsViewModel : ObservableRecipient
{
    private readonly IThemeSelectorService _themeSelectorService;
    private readonly IExplorerService _explorerService;
    private ElementTheme _elementTheme;
    private string _versionDescription;
    private string _copyrightDescription;

    public ElementTheme ElementTheme
    {
        get => _elementTheme;
        set => SetProperty(ref _elementTheme, value);
    }

    public string VersionDescription
    {
        get => _versionDescription;
        set => SetProperty(ref _versionDescription, value);
    }

    public string CopyrightDescription
    {
        get => _copyrightDescription;
        set => SetProperty(ref _copyrightDescription, value);
    }
    
    public ICommand SwitchThemeCommand
    {
        get;
    }

    public ICommand CmdEmailClick
    {
        get;
    }

    public ICommand CmdRestartExplorer
    {
        get;
    }

    public SettingsViewModel(IThemeSelectorService themeSelectorService, IExplorerService explorerService)
    {
        _themeSelectorService = themeSelectorService;
        _explorerService = explorerService;
        _elementTheme = _themeSelectorService.Theme;
        _versionDescription = GetVersionDescription();
        _copyrightDescription = GetCopyrightDescription();

        SwitchThemeCommand = new RelayCommand<ElementTheme>(
            async (param) =>
            {
                if (ElementTheme != param)
                {
                    ElementTheme = param;
                    await _themeSelectorService.SetThemeAsync(param);
                }
            });
        CmdEmailClick = new RelayCommand(EmailClick);
        CmdRestartExplorer = new RelayCommand(RestartExplorer);
    }

    public void RestartExplorer()
    {
        try
        {
            _explorerService.Restart();
        }
        catch(Exception ex) { }
    }

    private async void EmailClick()
    {
        await Windows.System.Launcher.LaunchUriAsync(new Uri(string.Format("mailto:{0}?subject={1} {2}&body={3}", "chansoftwaresolutions@gmail.com", "Classic Toolbar Support", GetVersionDescription(), "")));
    }

    private static string GetCopyrightDescription()
    {
        int year = DateTime.Now.Year;

        return $"© {year} Chan Software Solutions";
    }
    private static string GetVersionDescription()
    {
        Version version;

        if (RuntimeHelper.IsMSIX)
        {
            var packageVersion = Package.Current.Id.Version;

            version = new(packageVersion.Major, packageVersion.Minor, packageVersion.Build, packageVersion.Revision);
        }
        else
        {
            version = Assembly.GetExecutingAssembly().GetName().Version!;
        }

        return $"{"AppDisplayName".GetLocalized()} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
    }
}
