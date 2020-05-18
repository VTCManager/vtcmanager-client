using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTC_WPF.Klassen
{
    class TelemetryInstaller
    {
        private static string ETS2Folder64 = @"win_x64\";
        private static string ETS2Folder86 = @"win_x86\";
        private static string ETS2_EXE = "eurotrucks2.exe";
        public static void install()
        {
            String TelemetryPath = RegistryHandler.Globalread(Config.ETS2InstallFolderRegistryEntry, Config.ETS2InstallFolderRegistryEntryValue).ToString();
            if (!String.IsNullOrWhiteSpace(TelemetryPath))
            {
                TelemetryPath = TelemetryPath.Replace(ETS2_EXE, "");
                //remove the folder containing the exe from the path (both options can be used so I use 64 AND 86)
                TelemetryPath = TelemetryPath.Replace(ETS2Folder64, "");
                TelemetryPath = TelemetryPath.Replace(ETS2Folder86, "");
                Console.WriteLine(TelemetryPath);
            }
        }
    }
}
