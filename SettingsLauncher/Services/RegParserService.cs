using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SettingsLauncher.Contracts.Services;
using SettingsLauncher.Helpers;

namespace SettingsLauncher.Services;
public class RegParserService : IRegParserService
{
    public Dictionary<string, List<string>> ParseSettingsReg(string fullFile)
    {
        // Add hkey to all the missing dwords
        var formatted1 = PrependDistributeLines(fullFile.ToStringArray(), "^\\[.*?\\]", "^;(c|b|z|i) ");
        // Find all the ;T and separate the ;s to its own sections
        var formatted2 = ParseForPages(formatted1);
        // Add ;s to all the settings
        var formatted3 = PrependDistributeLinesSection(formatted2, "^;s ", "^(\\[.*?\\])|;(a|y|e|t|w)\\s");
        // Split out sections to a dictionary
        var formatted4 = DivideSections(formatted3.ToList());

        return formatted4;
    }

    private IEnumerable<string> PrependDistributeLinesSection(List<string> allSections, string toDistribute, string match)
    {
        foreach (var section in allSections)
        {
            yield return PrependDistributeLines(section.ToStringArray(), toDistribute, match, true);
        }
    }

    private string PrependDistributeLines(string[] array, string toDistribute, string match, bool overlap = false) 
    {
        StringBuilder result = new StringBuilder();

        HashSet<string> sectionHeaders = new HashSet<string>();

        for (int i = 0; i < array.Length; i++)
        {
            if (Regex.IsMatch(array[i], toDistribute))
            {
                if (!overlap)
                {
                    sectionHeaders = new HashSet<string>();
                }
                sectionHeaders.Add(array[i]);
                continue;
            }

            if (Regex.IsMatch(array[i], match))
            {
                result.AppendLine(string.Join("\r\n", sectionHeaders));

            }

            result.AppendLine(array[i]);
        }
        return result.ToString();
    }


    private Dictionary<string, List<string>> DivideSections(List<string> allSections)
    {
        Dictionary<string, List<string>> divided = new Dictionary<string, List<string>>();
        List<string> cumulativeSettings = new List<string>();

        foreach (var section in allSections)
        {
            var title = Regex.Match(section, ";T (?<TITLE>.*?)\r\n(?<BODY>.*)", RegexOptions.Singleline);
            if (title.Success)
            {
                cumulativeSettings = new List<string>() { title.Groups["BODY"].ToString() };
                divided.Add(title.Groups["TITLE"].ToString(), cumulativeSettings);
            }
            else
            {
                cumulativeSettings.Add(section);
            }
        }

        return divided;
    }

    private List<string> ParseForPages(string fullFile)
    {
        int sectionTracker = 0;
        List<string> sections = new List<string>();

        foreach (Match group in Regex.Matches(fullFile, "(?<=\\;T )(?<SETTING>.*?)(?=\\;T )", RegexOptions.Singleline))
        {
            StringBuilder sectionString = new StringBuilder();

            var settingArea = String.Format("{0} {1}", ";T", group.Groups["SETTING"].ToString());
            string[] array = settingArea.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            for (var i = 0; i < array.Length; i++)
            {
                if (Regex.IsMatch(array[i], "^\\;s "))
                {
                    ++sectionTracker;
                    if (sectionTracker == 1) //hit a new section, add all the previous ones to this
                    {
                        if (!String.IsNullOrWhiteSpace(sectionString.ToString()))
                        {
                            sections.Add(sectionString.ToString());
                        }
                        sectionString = new StringBuilder();
                    }

                    sectionString.AppendLine(array[i]);
                }
                else if (Regex.IsMatch(array[i], "^\\;g "))
                {
                    sectionString.AppendLine(array[i]);

                    --sectionTracker;
                    if (sectionTracker == 0)
                    {
                        sections.Add(sectionString.ToString());
                        sectionString = new StringBuilder();
                    }
                }
                else
                {
                    sectionString.AppendLine(array[i]);
                }
            }

            if (!String.IsNullOrWhiteSpace(sectionString.ToString()))
            {
                sections.Add(sectionString.ToString());
            }
        }

        return sections;
    }

}

