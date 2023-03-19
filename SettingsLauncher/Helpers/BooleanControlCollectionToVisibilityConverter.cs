using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml;
using System.Collections;
using SettingsLauncher.Contracts.Models;

namespace SettingsLauncher.Helpers;
public class BooleanControlCollectionToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if(value == null || (value as IList).Count == 0)
        {
            return Visibility.Visible;
        }

        IList<ILogical> controls = value as IList<ILogical>;

        foreach (ILogical c in controls)
        {
            if (!c.IsEnabled)
            {
                return Visibility.Collapsed;
            }
        }

        return Visibility.Visible;
    }


    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        //throw new NotImplementedException();
        return null;
    }

}

