// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ClassicTaskbarWrapper;
/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        this.InitializeComponent();
    }


    /// <summary>
    /// Invoked when the application is launched.
    /// </summary>
    /// <param name="args">Details about the launch request and process.</param>
    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        //if (!await App.GetService<ILocalSettingsService>().ReadSettingAsync<bool>("IsLaterRun"))
        //{
        //    //await App.GetService<ILocalSettingsService>().SaveSettingAsync("IsLaterRun", true);
        //}

        //check if ep_setup is installed. if it is not, then open the window
        //what to do about double installed path?

        m_window = new MainWindow(500, 400);
        m_window.Activate(); 
        return;

        string programFiles = System.IO.Path.Combine(Environment.ExpandEnvironmentVariables("%ProgramW6432%"), "ClassicTaskbar", "SettingsLauncher.exe");

        if (File.Exists(programFiles))
        {
            var process = Process.Start($"{programFiles}");


            //todo - make a really small dummy window
            //bug with winui where the application is unable to exit unless there is a window
            //https://github.com/microsoft/microsoft-ui-xaml/issues/5931
            m_window = new MainWindow(1, 1);
            m_window.Activate();
            Application.Current.Exit();
        }
        else
        {
            m_window = new MainWindow(500, 400);
            m_window.Activate();
        }
    }

    private Window m_window;
}
