using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SettingsLauncher.Contexts;
using SettingsLauncher.Contracts.Models;

namespace SettingsLauncher.Models.Parsers;
public class LabelParser : ParserBase, ISettingParser
{
    public LabelParser(RegLogicalsContext regLogicalsContext)
        : base(regLogicalsContext) { }

    public IControl ParseData(string data)
    {
        return new LabelControl()
        {
            Label = Regex.Match(data, ";(t|a)\\s(?<LABEL>.*)").Groups["LABEL"].Value,
            LogicalIsVisibles = GetLogicals(data)
        };
    }
}
