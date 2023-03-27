using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using SettingsLauncher.Contracts.Models;
using SettingsLauncher.Core.Models.Enums;

namespace SettingsLauncher.Models;
public class HyperLinkControl : ControlBase, IControl
{
    public ICommand LaunchURICommand
    {
        get;
    }

    public HyperLinkControl()
    {
        LaunchURICommand = new RelayCommand(LaunchURI);
    }

    public SettingType ControlType => SettingType.Link;

    public string URIAddress
    {
        get => _urlAddress;
        set => SetProperty(ref _urlAddress, value);
    }
    private string _urlAddress;

    private async void LaunchURI()
    {
        if (!IsUrlValid(this.URIAddress))
        {
            try
            {
                System.Diagnostics.Process.Start("explorer.exe", this.URIAddress);
            }
            catch { }
        }
    }
    private bool IsUrlValid(string url)
    {
        string pattern = @"^(http|https|ftp|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$";
        Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        return reg.IsMatch(url);
    }

}
