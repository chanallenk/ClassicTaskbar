using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SettingsLauncher.Core.Models;

namespace SettingsLauncher.Contracts.Services;
public interface IRegParserService
{
    Dictionary<string, List<string>> ParseSettingsReg(string fullFile);

}
