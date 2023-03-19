using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsLauncher.Helpers;
public static class GenericExtensions
{
    public static int ToInt(this string input)
    {
        int convertedInput;
        double convertedInput2;

        if (int.TryParse(input, out convertedInput))
        {
            return convertedInput;
        }
        else if (double.TryParse(input, out convertedInput2))
        {
            return (int)convertedInput2;
        }
        else
        {
            return 0;
        }
    }
    public static double ToDouble(this string input)
    {
        double convertedInput;
        int convertedInput2;

        if (double.TryParse(input, out convertedInput))
        {
            return convertedInput;
        }
        else if (int.TryParse(input, out convertedInput2))
        {
            return convertedInput2;
        }
        else
        {
            return 0;
        }
    }
    public static bool ToBool(this string input)
    {
        bool convertedInput = true;

        if (bool.TryParse(input, out convertedInput))
        {
            return convertedInput;
        }
        else
        {
            return true;
        }
    }
}
