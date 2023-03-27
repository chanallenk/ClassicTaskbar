// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.Threading;
using WinUIEx;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ClassicTaskbarWrapper;
/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    public MainWindow(int width, int height)
    {
        this.InitializeComponent();

        this.SetWindowSize(500, 400);
        this.CenterOnScreen();
        this.Title = "Classic Taskbar Installer";
        this.ExtendsContentIntoTitleBar = true;

        var manager = WinUIEx.WindowManager.Get(this);
        manager.Backdrop = new WinUIEx.MicaSystemBackdrop();
        //manager.IsTitleBarVisible = false;

        ThreadPoolTimer timer = ThreadPoolTimer.CreatePeriodicTimer((t) =>
        {
            this.DispatcherQueue.TryEnqueue(() =>
            {
                ResetVisibilityContent();
            });
        }, TimeSpan.FromSeconds(3));

        ResetVisibilityContent();
    }

    private bool IsEPInstalled()
    {
        try
        {
            string programFiles = System.IO.Path.Combine(Environment.ExpandEnvironmentVariables("%ProgramW6432%"), "ClassicTaskbar", "ep_setup.exe");

            if (File.Exists(programFiles))
            {
                return true;
            }
        }
        catch { }
        return false;
    }

    private void ResetVisibilityContent()
    {
        if (IsEPInstalled())
        {
            UninstallButton.Visibility = Visibility.Visible;
            SettingsButton.Visibility = Visibility.Visible;
            InstallButton.Visibility = Visibility.Collapsed;
        }
        else
        {
            UninstallButton.Visibility = Visibility.Collapsed;
            SettingsButton.Visibility = Visibility.Collapsed;
            InstallButton.Visibility = Visibility.Visible;
        }

    }

    private void InstallClicked(object sender, RoutedEventArgs e)
    {
        try
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            string executableLocation = Path.GetDirectoryName(path) ?? string.Empty;
            var process = Process.Start($"{executableLocation}\\Executable\\ep_setup.exe");
        }
        catch { }

    }


    private async void UninstallClicked(object sender, RoutedEventArgs e)
    {
        try
        {
            string programFiles = System.IO.Path.Combine(Environment.ExpandEnvironmentVariables("%ProgramW6432%"), "ClassicTaskbar", "ep_setup.exe");

            if (File.Exists(programFiles))
            {
                var process = Process.Start($"{programFiles}", "/uninstall");
            }
            else
            {
                ContentDialog dialog = new ContentDialog
                {
                    Title = "Error",
                    Content = "Could not uninstall. Install path is missing files",
                    CloseButtonText = "Ok"
                };
                dialog.XamlRoot = this.Content.XamlRoot;
                ContentDialogResult result = await dialog.ShowAsync();
            }
        }
        catch { }

    }

    private async void SettingsClicked(object sender, RoutedEventArgs e)
    {
        try
        {

            ContentDialog dialog = new ContentDialog
            {
                Title = "",
                Content = "You can also access these settings using Right Click Taskbar > Properties",
                CloseButtonText = "Ok"
            };
            dialog.XamlRoot = this.Content.XamlRoot;
            ContentDialogResult result = await dialog.ShowAsync();

            string programFiles = System.IO.Path.Combine(Environment.ExpandEnvironmentVariables("%ProgramW6432%"), "ClassicTaskbar", "SettingsLauncher.exe");

            if (File.Exists(programFiles))
            {
                var process = Process.Start($"{programFiles}");
            }
        }
        catch { }
    }
}
