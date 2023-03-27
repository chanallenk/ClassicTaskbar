using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using SettingsLauncher.Contracts.Models;
using SettingsLauncher.Core.Models.Enums;

namespace SettingsLauncher.Views.Controls;
public class SettingDataTemplateSelector : DataTemplateSelector
{
    public DataTemplate CheckBox
    {
        get; set;
    }
    public DataTemplate ComboBox
    {
        get; set;
    }
    public DataTemplate HyperLink
    {
        get; set;
    }
    public DataTemplate Label
    {
        get; set;
    }

    protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
    {
        if (item == null)
        {
            return Label;
        }

        var settingType = ((IControl)item).ControlType;

        switch (settingType)
        {
            case SettingType.CheckBox:
                return CheckBox;
            case SettingType.DropDown:
                return ComboBox;
            case SettingType.Link:
                return HyperLink;
            case SettingType.Label:
                return Label;
            default: 
                return base.SelectTemplateCore(item, container);
        }

    }
}
