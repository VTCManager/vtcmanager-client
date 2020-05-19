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
        public static object Globalread(string registryPath, string valueName)
        {
            try
            {
                if (registryPath.Contains("HKEY_CURRENT_USER"))
                {
                    registryPath = registryPath.Replace(@"HKEY_CURRENT_USER\", "");
                    Console.WriteLine(registryPath);
                    RegistryKey key = Registry.CurrentUser.OpenSubKey(registryPath);
                    return key.GetValue(valueName);
                }
                else
                {
                    return null;
                }
            }
            catch (System.Security.SecurityException ex)
            {
                MessageBox.Show("Failed to load RegValue: " + registryPath + valueName + "\n SecurityException:" + ex.Message);
                return null;
            }
            catch (NullReferenceException ex)
            {
                return null;
            }
        }


        public string Reg_lesen(string ordner, string value)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\VTCManager\" + ordner);
                return key.GetValue(value).ToString();
            } catch (Exception ex)
            {
            
                return null;
            }

        }
    }
}
