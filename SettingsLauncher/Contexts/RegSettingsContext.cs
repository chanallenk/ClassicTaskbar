using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using SettingsLauncher.Contracts.Models;
using SettingsLauncher.Core.Models.Enums;
using SettingsLauncher.Models.Parsers;

namespace SettingsLauncher.Contexts;
public class RegSettingsContext
{
    private readonly List<Tuple<string, SettingType, ISettingParser>> parseStrategies = new();
    private readonly RegExclusionsContext _regExclusionsContext;
    private readonly RegLogicalsContext _regLogicalsContext;

    public RegSettingsContext(RegExclusionsContext regExclusionsContext, RegLogicalsContext regLogicalsContext)
    {
        _regExclusionsContext = regExclusionsContext;
        _regLogicalsContext = regLogicalsContext;
        parseStrategies.Add(new Tuple<string, SettingType, ISettingParser>(";x\\s", SettingType.DropDown, new DropDownParser(regLogicalsContext)));
        parseStrategies.Add(new Tuple<string, SettingType, ISettingParser>(";(t|a)\\s", SettingType.Label, new LabelParser(regLogicalsContext)));
        parseStrategies.Add(new Tuple<string, SettingType, ISettingParser>(";(b|i)\\s", SettingType.CheckBox, new CheckBoxParser(regLogicalsContext)));
        parseStrategies.Add(new Tuple<string, SettingType, ISettingParser>(";y\\s", SettingType.Link, new LinkParser(regLogicalsContext)));
    }

    public IControl? GetParsedSetting(string data)
    {
        if (Regex.IsMatch(data, "when right clicking", RegexOptions.IgnoreCase))
        {
        
        }

        Match hkey = Regex.Match(data, "\"(?<KEY>.*)\"=dword:(?<VALUE>[0-9a-zA-Z]*)");
        if (_regExclusionsContext.IsExcluded(data))
        {
            return null;
        }

        return parseStrategies.FirstOrDefault(p => Regex.IsMatch(data, p.Item1))?.Item3.ParseData(data);
    }
}
