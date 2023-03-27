using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsLauncher.Helpers;
public static class StringExtensions
{
    public static List<string> ToStringList(this string value)
    {
        return value.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    public static string[] ToStringArray(this string value)
    {
        return value.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
    }

    public static string ToSingleString(this List<string> value)
    {
        return string.Join("\r\n", value.ToArray());
    }

    public static string RemoveWhitespace(this string input)
    {
        return new string(input.ToCharArray()
            .Where(c => !Char.IsWhiteSpace(c))
            .ToArray());
    }
}
