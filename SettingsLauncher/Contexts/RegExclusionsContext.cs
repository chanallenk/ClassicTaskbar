using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SettingsLauncher.Contexts;
public class RegExclusionsContext
{
    private readonly List<string> Matches = new()
    {
        ";\"Virtualized",
        "\\[-HKEY",
        ";shell:::\\{05d7b0f4-2121-4eff-bf6b-ed3f69b894d9\\}\\\\SystemIcons"
    };

    public bool IsExcluded(string data)
    {
        if (this.Matches.Any(p => Regex.IsMatch(data, p)))
        {
            return true;
        }
        return false;
    }
}
