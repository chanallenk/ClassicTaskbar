using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SettingsLauncher.Contexts;
using SettingsLauncher.Contracts.Models;

namespace SettingsLauncher.Models.Parsers;
public class ParserBase
{
    private readonly RegLogicalsContext _regLogicalsContext;
    public ParserBase(RegLogicalsContext regLogicalsContext)
    {
        _regLogicalsContext = regLogicalsContext;
    }

    public ObservableCollection<ILogical> GetLogicals(string data)
    {
        var logicalResults = Regex.Matches(data, ";s [a-z].* (?<BOOL>.*)", RegexOptions.IgnoreCase).Select(p => p.Groups["BOOL"].ToString()).ToList();

        List<ILogical> logicals = logicalResults.Select(p => _regLogicalsContext.GetLogical(p)).Where(o => o != null).ToList();

        return logicals != null ? new ObservableCollection<ILogical>(logicals) : new();
    }
}
