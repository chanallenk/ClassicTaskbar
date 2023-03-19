using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsLauncher.Contracts.Models;
public interface ISettingParser
{
    IControl ParseData(string data);
}
