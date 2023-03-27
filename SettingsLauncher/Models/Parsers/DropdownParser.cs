using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using SettingsLauncher.Contexts;
using SettingsLauncher.Contracts.Models;
using SettingsLauncher.Helpers;

namespace SettingsLauncher.Models.Parsers;
public class DropDownParser : ParserBase, ISettingParser
{
    public DropDownParser(RegLogicalsContext regLogicalsContext)
        : base(regLogicalsContext) { }

    public IControl ParseData(string data)
    {
        Match hkey = Regex.Match(data, "\"(?<KEY>.*)\"=dword:(?<VALUE>[0-9a-zA-Z]*)");

        var dwordVal = hkey.Groups["VALUE"].ToString();
        int dwordInt = dwordVal.ToInt();
        dwordInt = Convert.ToInt32(dwordVal, 16);

        DropDownControl dropdownControl = new DropDownControl()
        {
            Label = Regex.Match(data, ";(c|z)\\s[0-9]*\\s(?<LABEL>.*)").Groups["LABEL"].ToString(),
            RegistryPath = Regex.Match(data, "^\\[(?<HKEY>.*)\\]", RegexOptions.Multiline).Groups["HKEY"].ToString(),
            RegistryKey = hkey.Groups["KEY"].Value,
            DropdownItems = Regex.Matches(data, ";x (?<ITEMVALUE>.*?) (?<TEXT>.*)")
                .Select(p => new KeyValuePair<string, int>(p.Groups["TEXT"].ToString(), p.Groups["ITEMVALUE"].ToString().ToInt()))
                .ToDictionary(p => p.Key, q => q.Value),
            _selectedValue = dwordInt,
            LogicalIsVisibles = GetLogicals(data)
        };

        return dropdownControl;
    }
}
