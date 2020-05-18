using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VTC_WPF.Klassen
{
    class RegistryHandler
    {
        public static object read(string registryPath, string valueName)
        {
            try
            {
                if (registryPath.Contains("HKEY_LOCAL_MACHINE"))
                {
                    registryPath = registryPath.Replace(@"HKEY_LOCAL_MACHINE\", "");
                    Console.WriteLine(registryPath);
                    RegistryKey key = Registry.LocalMachine.OpenSubKey(registryPath);
                    foreach(String key2 in key.GetSubKeyNames())
                    {
                        Console.WriteLine(key2);
                    }
                    Console.WriteLine(key.GetValue(valueName));
                    return key.GetValue(valueName);
                }
                return null;
            }
            catch (System.Security.SecurityException ex)
            {
                MessageBox.Show("Failed to load RegValue: " + registryPath + valueName + "\n SecurityException:" + ex.Message);
                return null;
            }
        }
    }
}
