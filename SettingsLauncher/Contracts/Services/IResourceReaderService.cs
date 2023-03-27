using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsLauncher.Contracts.Services;
public interface IResourceReaderService
{
    string ReadResourceAsString(string resourceName);

    string GetAssemblyPath();
}
