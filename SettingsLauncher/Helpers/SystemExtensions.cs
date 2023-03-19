using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SettingsLauncher.Helpers;
public static class SystemExtension
{
    public static T Clone<T>(this T source)
    {
        var serialized = JsonConvert.SerializeObject(source);
        return JsonConvert.DeserializeObject<T>(serialized);
    }
}
