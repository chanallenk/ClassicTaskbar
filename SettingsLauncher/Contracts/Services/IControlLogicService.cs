using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SettingsLauncher.Contracts.Models;
using SettingsLauncher.Contracts.ViewModels;
using SettingsLauncher.Core.Models;

namespace SettingsLauncher.Contracts.Services;
public interface IControlLogicService
{
    List<IControl> GetControls<TViewModel>();

    Dictionary<Type, List<IControl>> GenerateSettings(Dictionary<string, List<string>> regLines);
}
