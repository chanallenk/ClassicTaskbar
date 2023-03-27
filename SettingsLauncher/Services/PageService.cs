using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.UI.Xaml.Controls;

using SettingsLauncher.Contracts.Services;
using SettingsLauncher.Contracts.ViewModels;
using SettingsLauncher.ViewModels;
using SettingsLauncher.Views;

namespace SettingsLauncher.Services;

public class PageService : IPageService
{
    private readonly Dictionary<string, Type> _pages = new();

    public PageService()
    {
        Configure<MainViewModel, MainPage>();
        Configure<SettingsViewModel, SettingsPage>();
        Configure<TaskbarViewModel, TaskbarPage>();
        Configure<SystemTrayViewModel, SystemTrayPage>();
        Configure<FileExplorerViewModel, FileExplorerPage>();
        Configure<StartMenuViewModel, StartMenuPage>(); 
        Configure<WindowSwitcherViewModel, WindowSwitcherPage>();

        Configure<WeatherViewModel, WeatherPage>();
        Configure<OtherViewModel, OtherPage>();
        Configure<UpdatesViewModel, UpdatesPage>();
        Configure<AdvancedViewModel, AdvancedPage>();
        Configure<AboutViewModel, AboutPage>();
    }

    public Type GetPageType(string key)
    {
        Type? pageType;
        lock (_pages)
        {
            if (!_pages.TryGetValue(key, out pageType))
            {
                throw new ArgumentException($"Page not found: {key}. Did you forget to call PageService.Configure?");
            }
        }

        return pageType;
    }

    private void Configure<VM, V>()
        where VM : ObservableObject
        where V : Page
    {
        lock (_pages)
        {
            var key = typeof(VM).FullName!;
            if (_pages.ContainsKey(key))
            {
                throw new ArgumentException($"The key {key} is already configured in PageService");
            }

            var type = typeof(V);
            if (_pages.Any(p => p.Value == type))
            {
                throw new ArgumentException($"This type is already configured with key {_pages.First(p => p.Value == type).Key}");
            }

            _pages.Add(key, type);
        }
    }


    private IEnumerable<Type> GetTypes<TControl>()
    {
        var type = typeof(TControl);

        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p));

        return types;
    }


}
