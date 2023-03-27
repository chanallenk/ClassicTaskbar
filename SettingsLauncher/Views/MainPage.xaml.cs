using Microsoft.UI.Xaml.Controls;

using SettingsLauncher.ViewModels;

namespace SettingsLauncher.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();
    }
}
