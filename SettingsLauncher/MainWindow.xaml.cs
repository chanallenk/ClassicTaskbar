using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI;
using SettingsLauncher.Helpers;
using Windows.Graphics;
using WinRT.Interop;

namespace SettingsLauncher;

public sealed partial class MainWindow : WindowEx
{
    private bool centered;
    public MainWindow()
    {
        InitializeComponent();

        this.SetWindowSize(1300, 800);
        this.CenterOnScreen();

        AppWindow.SetIcon(Path.Combine(AppContext.BaseDirectory, "Assets/StartIcon.ico"));
        Content = null;
        Title = "AppDisplayName".GetLocalized();
    }
}
