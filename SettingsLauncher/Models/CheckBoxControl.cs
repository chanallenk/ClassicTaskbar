using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using SettingsLauncher.Contracts.Models;
using SettingsLauncher.Core.Models.Enums;
using static SettingsLauncher.Contracts.Models.IRegistryAction;

namespace SettingsLauncher.Models;
public class CheckBoxControl : ControlBase, IControl, IRegistryAction
{
    public SettingType ControlType => SettingType.CheckBox;

    public bool IsChecked
    {
        get => _isChecked;
        set 
        {
            if (_isChecked != value)
            {
                SetRegistryValue?.Invoke(this.RegistryPath, this.RegistryKey, value ? 1 : 0);
            }
            
            SetProperty(ref _isChecked, value);
        }
    }
    public bool _isChecked;

    public void LoadRegistryValue(Func<string, string, object> tryLoadRegistrySettings)
    {
        if (Regex.IsMatch(this.RegistryFullKeyPath, "showseconds", RegexOptions.IgnoreCase | RegexOptions.Multiline))
        {
            //issue with dropdowns in windowswticher -> need to test in a separate control - is the issue with any numbers as the key? or spaces in the key?
        }

        bool? result = null;
        var regVal = tryLoadRegistrySettings(this.RegistryPath, this.RegistryKey);
        if (regVal != null) 
        {
            if (regVal.ToString() == "0")
            {
                result = false;
            }
            else if (regVal.ToString() == "1")
            {
                result = true;
            }
        }
        this.IsChecked = result ?? this.IsChecked;
    }
}
