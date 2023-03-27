using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Shapes;
using SettingsLauncher.Contexts;
using SettingsLauncher.Contracts.Models;
using SettingsLauncher.Helpers;

namespace SettingsLauncher.Models.Parsers;
public class CheckBoxParser : ParserBase, ISettingParser
{
    public CheckBoxParser(RegLogicalsContext regLogicalsContext)
        : base(regLogicalsContext) { }

    public IControl ParseData(string data)
    {
        Match hkey = Regex.Match(data, "\"(?<KEY>.*)\"=dword:(?<VALUE>[0-9a-z]*)");

        CheckBoxControl control = new CheckBoxControl()
        {
            Label = Regex.Match(data, ";(b|i)\\s(?<LABEL>.*)").Groups["LABEL"].ToString(),
            RegistryPath = Regex.Match(data, "^\\[(?<HKEY>.*)\\]", RegexOptions.Multiline).Groups["HKEY"].ToString(),
            RegistryKey = hkey.Groups["KEY"].Value,
            _isChecked = hkey.Groups["VALUE"].Value == "1",
            LogicalIsVisibles = GetLogicals(data)
        };

        return control;
    }
}