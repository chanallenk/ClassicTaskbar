using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SettingsLauncher.Contracts.Models;
using SettingsLauncher.Core.Models.Enums;
using SettingsLauncher.Models.LogicalControls;
using SettingsLauncher.Models.Parsers;

namespace SettingsLauncher.Contexts;
public class RegLogicalsContext
{
    private readonly Dictionary<string, Type> logicalItems = new();

    public RegLogicalsContext()
    {
        logicalItems.Add("IsOldTaskbar", typeof(OldTaskBarLogicalControl));
        logicalItems.Add("IsSWSEnabled", typeof(SWSEnabledLogicalControl));
        logicalItems.Add("IsWeatherEnabled", typeof(WeatherEnabledLogicalControl));
        logicalItems.Add("IsWindows10StartMenu", typeof(Windows10StartMenuLogicalControl));
        logicalItems.Add("IsWindows11Version22H2OrHigher", typeof(Windows11Version22H2OrHigherLogicalControl));
    }

    public ILogical? GetLogical(string condition)
    {
        Match m = Regex.Match(condition, "(?<INVERT>\\!?)(?<CONDITION>.*)");

        logicalItems.TryGetValue(m.Groups["CONDITION"].ToString().Trim(), out var logical);

        if (logical != null)
        {
            ILogical? instance = (ILogical)Activator.CreateInstance(logical);

            if (instance != null)
            {
                instance.Inverted = !String.IsNullOrWhiteSpace(m.Groups["INVERT"].ToString());
                return instance;
            }
        }

        return null;
    }
}
