using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTC_WPF.Klassen
{
    class TelemetryInstaller
    {
        public static void install()
        {
            Console.WriteLine("Value: "+RegistryHandler.read(Config.ETS2InstallFolderRegistryEntry, Config.ETS2InstallFolderRegistryEntryValue));
        }
    }
}
