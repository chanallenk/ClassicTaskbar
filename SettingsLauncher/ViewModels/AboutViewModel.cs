using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SettingsLauncher.Contracts.Models;
using SettingsLauncher.Contracts.Services;
using SettingsLauncher.Core.Contracts.Services;
using SettingsLauncher.ViewModels.Core;

namespace SettingsLauncher.ViewModels
{
    public class AboutViewModel : CorePropertiesViewModelBase
    {
        public AboutViewModel(IRegistryService registryService, IControlLogicService controlLogicService)
            : base(registryService, controlLogicService)
        {
        }

        public override ObservableCollection<IControl> Controls => new(_controlLogicService.GetControls<AboutViewModel>());

    }
}
