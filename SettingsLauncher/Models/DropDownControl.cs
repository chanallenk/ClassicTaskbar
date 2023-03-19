using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using SettingsLauncher.Contracts.Models;
using SettingsLauncher.Core.Models.Enums;

namespace SettingsLauncher.Models;
public class DropDownControl : ControlBase, IControl, IRegistryAction
{
    public SettingType ControlType => SettingType.DropDown;

    public int SelectedValue
    {
        get => _selectedValue;
        set  
        {
            if (_selectedValue != value)
            {
                SetRegistryValue?.Invoke(this.RegistryPath, this.RegistryKey, value);
            }
            SetProperty(ref _selectedValue, value);
        }
    }
    public int _selectedValue;

    public Dictionary<string, int> DropdownItems
    {
        get => _dropdownItems;
        set => SetProperty(ref _dropdownItems, value);
    }
    private Dictionary<string, int> _dropdownItems;

    public void LoadRegistryValue(Func<string, string, object> tryLoadRegistrySettings)
    {
        this.SelectedValue = (int?)tryLoadRegistrySettings(this.RegistryPath, this.RegistryKey) ?? this.SelectedValue;
    }
}
