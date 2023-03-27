using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsLauncher.Helpers;
public class TypesHelper
{
    public static IEnumerable<Type> GetTypes<TControl>()
    {
        var type = typeof(TControl);

        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p));

        return types;
    }
}
