using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;

using SettingsLauncher.Activation;
using SettingsLauncher.Contexts;
using SettingsLauncher.Contracts.Services;
using SettingsLauncher.Core.Contracts.Services;
using SettingsLauncher.Core.Services;
using SettingsLauncher.Helpers;
using SettingsLauncher.Models;
using SettingsLauncher.Services;
using SettingsLauncher.ViewModels;
using SettingsLauncher.Views;

namespace SettingsLauncher;

// To learn more about WinUI 3, see https://docs.microsoft.com/windows/apps/winui/winui3/.
public partial class App : Application
{
    // The .NET Generic Host provides dependency injection, configuration, logging, and other services.
    // https://docs.microsoft.com/dotnet/core/extensions/generic-host
    // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
    // https://docs.microsoft.com/dotnet/core/extensions/configuration
    // https://docs.microsoft.com/dotnet/core/extensions/logging
    public IHost Host
    {
        get;
    }

    public static T GetService<T>()
        where T : class
    {
        if ((App.Current as App)!.Host.Services.GetService(typeof(T)) is not T service)
        {
            throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.");
        }

        return service;
    }

    public static WindowEx MainWindow { get; } = new MainWindow();

    public App()
    {
        InitializeComponent();

        Host = Microsoft.Extensions.Hosting.Host.
        CreateDefaultBuilder().
        UseContentRoot(AppContext.BaseDirectory).
        ConfigureServices((context, services) =>
        {
            // Default Activation Handler
            services.AddTransient<ActivationHandler<LaunchActivatedEventArgs>, DefaultActivationHandler>();

            // Other Activation Handlers

            // Services
            services.AddSingleton<ILocalSettingsService, LocalSettingsService>();
            services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
            services.AddTransient<INavigationViewService, NavigationViewService>();

            services.AddSingleton<IActivationService, ActivationService>();
            services.AddSingleton<IPageService, PageService>(); //possibly inherit the pageservice from a parent class
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IRegParserService, RegParserService>();
            services.AddTransient<IExplorerService, ExplorerService>();


            // Core Services

            services.AddSingleton<IFileService, FileService>();
#if !DEBUG
            services.AddTransient<IRegistryService, RegistryService>(); //Prod
#else
            services.AddSingleton<IRegistryService, FileWriteRegService>(); //Test
#endif
            services.AddTransient<IResourceReaderService, ResourceReaderService>();
            services.AddSingleton<IControlLogicService, ControlLogicService>();

            // Views and ViewModels
            services.AddTransient<SettingsViewModel>();
            services.AddTransient<SettingsPage>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<MainPage>();
            services.AddTransient<ShellPage>();
            services.AddTransient<ShellViewModel>();

            services.AddTransient<TaskbarPage>();
            services.AddTransient<TaskbarViewModel>();
            services.AddTransient<SystemTrayPage>();
            services.AddTransient<SystemTrayViewModel>();
            services.AddTransient<FileExplorerPage>();
            services.AddTransient<FileExplorerViewModel>();
            services.AddTransient<StartMenuPage>();
            services.AddTransient<StartMenuViewModel>();
            services.AddTransient<WindowSwitcherPage>();
            services.AddTransient<WindowSwitcherViewModel>();
            services.AddTransient<WeatherPage>();
            services.AddTransient<WeatherViewModel>();
            services.AddTransient<OtherPage>();
            services.AddTransient<OtherViewModel>();
            services.AddTransient<UpdatesPage>();
            services.AddTransient<UpdatesViewModel>();
            services.AddTransient<AdvancedPage>();
            services.AddTransient<AdvancedViewModel>();
            services.AddTransient<AboutPage>();
            services.AddTransient<AboutViewModel>();

            services.AddTransient<RegSettingsContext>();
            services.AddTransient<RegExclusionsContext>();
            services.AddTransient<RegLogicalsContext>();

            // Configuration
            services.Configure<LocalSettingsOptions>(context.Configuration.GetSection(nameof(LocalSettingsOptions)));
        }).
        Build();

        UnhandledException += App_UnhandledException;
    }

    private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
    {
        // TODO: Log and handle exceptions as appropriate.
        // https://docs.microsoft.com/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.application.unhandledexception.
    }

    protected async override void OnLaunched(LaunchActivatedEventArgs args)
    {
        base.OnLaunched(args);

        await App.GetService<IActivationService>().ActivateAsync(args);

        App.GetService<IControlLogicService>()
            .GenerateSettings(
            App.GetService<IRegParserService>()
                .ParseSettingsReg(
                App.GetService<IResourceReaderService>()
                    .ReadResourceAsString("SettingsLauncher.settings.reg")));

        if (!await App.GetService<ILocalSettingsService>().ReadSettingAsync<bool>("IsLaterRun"))
        {
            //await App.GetService<ILocalSettingsService>().SaveSettingAsync("IsLaterRun", true);
        }

        App.GetService<INavigationService>().NavigateTo(typeof(TaskbarViewModel).FullName!, args.Arguments);
        
    }
}
