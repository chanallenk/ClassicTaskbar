using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SettingsLauncher.Contracts.Services;

namespace SettingsLauncher.Services;
public class ResourceReaderService : IResourceReaderService
{
    public string ReadResourceAsString(string resource)
    {
        var assembly = Assembly.GetExecutingAssembly();

        using (Stream stream = assembly.GetManifestResourceStream(resource))
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
    public string GetAssemblyPath()
    {
        string codeBase = Assembly.GetExecutingAssembly().CodeBase;
        UriBuilder uri = new UriBuilder(codeBase);
        string path = Uri.UnescapeDataString(uri.Path);
        return Path.GetDirectoryName(path) ?? string.Empty;
    }

}
