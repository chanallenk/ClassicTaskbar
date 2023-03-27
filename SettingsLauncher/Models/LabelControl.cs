using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using SettingsLauncher.Contracts.Models;
using SettingsLauncher.Core.Models.Enums;

namespace SettingsLauncher.Models;
public class LabelControl : ObservableRecipient, IControl
{
    public SettingType ControlType => SettingType.Label;

    public string Label
    {
        get => _label;
        set => SetProperty(ref _label, value);
    }
    private string _label;

    public ObservableCollection<ILogical> LogicalIsVisibles
    {
        get; set;
    } = new();

}
