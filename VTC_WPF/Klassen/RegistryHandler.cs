using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTC_WPF.Klassen
{
    class RegistryHandler
    {
        public static object read(string registryPath, string valueName)
        {
            try
            {
                return Registry.GetValue(registryPath, valueName, null);
            }
            catch (System.Security.SecurityException ex)
            {
                return null;
            }
        }
    }
}
