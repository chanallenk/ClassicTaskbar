using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SettingsLauncher.Contexts;
using SettingsLauncher.Contracts.Models;
using SettingsLauncher.Contracts.Services;
using SettingsLauncher.Contracts.ViewModels;
using SettingsLauncher.Core.Contracts.Services;
using SettingsLauncher.Core.Models;
using SettingsLauncher.Helpers;
using SettingsLauncher.Models;

namespace SettingsLauncher.Services;
public class ControlLogicService : IControlLogicService
{
    private readonly RegSettingsContext _regSettingContext;
    private readonly Dictionary<Type, List<IControl>> _controls = new();

    public List<IControl> GetControls<TViewModel>() => _controls.FirstOrDefault(p => p.Key == typeof(TViewModel)).Value;

    public ControlLogicService(RegSettingsContext regSettingContext)
    {
        this._regSettingContext = regSettingContext;
    }

    public Dictionary<Type, List<IControl>> GenerateSettings(Dictionary<string, List<string>> regLines)
    {
        var typesVM = TypesHelper.GetTypes<IControlLogicViewModel>();
        var returnSettings = new Dictionary<Type, List<IControl>>();
        var controls = new List<IControl>();

        foreach (var regSection in regLines)
        {
            List<IControl> settings = new List<IControl>();
            var compressedSectionName = regSection.Key.RemoveWhitespace();
            var viewModelType = typesVM.FirstOrDefault(p => Regex.IsMatch(p.FullName, compressedSectionName, RegexOptions.IgnoreCase));

            if (viewModelType != null)
            {
                foreach (var body in regSection.Value)
                {
                    foreach (Match m in Regex.Matches(body, "(;s .*?)?(\\[.*?\\]\\s*;(c|b|z|i) .*?=dword:(?<VALUE>[0-9a-zA-Z]*))|(;y .*?;.*?$)|(;e .*?$)|(;t .*?$)|(;a .*?$)|(;w .*?$)", RegexOptions.Singleline | RegexOptions.Multiline))
                    {
                        if (Regex.IsMatch(m.Value, "when right clicking", RegexOptions.IgnoreCase))
                        {

                        }

                        IControl? settingControlHeader = CheckExtraLogicalSubHeaders(m.Value);
                        if (settingControlHeader != null)
                        {
                            settings.Add(settingControlHeader);
                        }

                        IControl? settingControl = _regSettingContext.GetParsedSetting(m.Value);
                        if (settingControl != null)
                        {
                            settings.Add(settingControl);
                        }
                    }
                }

                returnSettings.Add(viewModelType, settings);
                _controls.Add(viewModelType, settings);
            }
        }
        return returnSettings;
    }

    private IControl? CheckExtraLogicalSubHeaders(string data)
    {
        Match m = Regex.Match(data, "(;s .*?)((;a .*?$)|(;t .*?$))", RegexOptions.Singleline | RegexOptions.Multiline);
        if (m.Success)
        {
            return _regSettingContext.GetParsedSetting(m.Value);
        }
        return null;
    }

}
