using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SettingsLauncher.Contexts;
using SettingsLauncher.Contracts.Models;

namespace SettingsLauncher.Models.Parsers;
public class LinkParser : ParserBase, ISettingParser
{
    public LinkParser(RegLogicalsContext regLogicalsContext)
        : base(regLogicalsContext) { }

    public IControl ParseData(string data)
    {
        Match linkMatch = Regex.Match(data, ";y\\s(?<LABEL>.*)\n;(?<LINK>.*)", RegexOptions.Singleline);
        HyperLinkControl control = new HyperLinkControl()
        {
            Label = linkMatch.Groups["LABEL"].ToString(),
            URIAddress = linkMatch.Groups["LINK"].ToString(),
            LogicalIsVisibles = GetLogicals(data)
        };

        return control;
    }
}