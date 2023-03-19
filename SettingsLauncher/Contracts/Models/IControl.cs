using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SettingsLauncher.Core.Models.Enums;

namespace SettingsLauncher.Contracts.Models;
public interface IControl
{
    SettingType ControlType
    {
        get;
    }

    ObservableCollection<ILogical> LogicalIsVisibles
    {
        get; set;
    }
}
