using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using SettingsLauncher.Contracts.Models;

namespace SettingsLauncher.Models.LogicalControls;
public abstract class LogicalControlBase : ObservableRecipient
{
    public bool IsEnabled => CanBeEnabled();

    public abstract bool CanBeEnabled();

}
