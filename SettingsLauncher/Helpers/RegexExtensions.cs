using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Storage;

namespace SettingsLauncher.Helpers;
public static class RegexExtensions
{
    public static string ToStringFromGroup(this Match match, string text)
    {
        return match.Groups[text].ToString();
    }
}
