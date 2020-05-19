using System;
using System.IO;
using System.Windows;

namespace VTC_WPF.Klassen
{
    class TelemetryInstaller
    {
        //ETS
        private static string ETS2Folder64 = @"win_x64\";
        private static string ETS2Folder86 = @"win_x86\";
        private static string ETS2_EXE = "eurotrucks2.exe";
        //STEAM
        private static string SteamLibraryConfigFile = @"\steamapps\libraryfolders.vdf";
        /*public static string ProgramPath;
        public static string ETSPath = @"\Steam\steamapps\common\Euro Truck Simulator 2";
        public static string ATSPath = @"\Steam\steamapps\common\American Truck Simulator";*/

        public static void install()
        {
            //detect SteamInstallPath
            String SteamInstallPath = RegistryHandler.Globalread(RegistryHandler.Steam64bitRegistry, RegistryHandler.SteamInstallPathValueName).ToString();
            if (!string.IsNullOrEmpty(SteamInstallPath))
            {
                String SteamLibraryConfigPath = SteamInstallPath + SteamLibraryConfigFile;
                if (File.Exists(SteamLibraryConfigPath))
                {

                }
            }

            String TelemetryPath = RegistryHandler.Globalread(Config.ETS2InstallFolderRegistryEntry, Config.ETS2InstallFolderRegistryEntryValue).ToString();
            if (!string.IsNullOrWhiteSpace(TelemetryPath))
            {
                TelemetryPath = TelemetryPath.Replace(ETS2_EXE, "");
                //remove the folder containing the exe from the path (both options can be used so I use 64 AND 86)
                TelemetryPath = TelemetryPath.Replace(ETS2Folder64, "");
                TelemetryPath = TelemetryPath.Replace(ETS2Folder86, "");
                File.Copy(@"Resources/scs-telemetry.dll", TelemetryPath + ETS2Folder64 + @"plugins\scs-telemetry.dll");
                File.Copy(@"Resources/scs-telemetry.dll", TelemetryPath + ETS2Folder86 + @"plugins\scs-telemetry.dll");
            }
        }

        /*public static void install2()
        {
            try
            {
                ProgramPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                if (Directory.Exists(ProgramPath + ETSPath) )
                {
                    MessageBox.Show("ETS2 wurde gefunden in: " + Environment.NewLine + ProgramPath + ETSPath, "Pfad von ETS erfolgreich gefunden !", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    // Kopiere dll in das Plugin Verzeichnis \bin\win_x64\plugins #### vorher muss geprüft werden ob plugins vorhanden ist ####
                    Logging.WriteClientLog("ETS2 wurde gefunden in: " + ProgramPath + ETSPath);
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show("ETS2 wurde nicht gefunden in " + ProgramPath + ETSPath + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                Logging.WriteClientLog("ETS wurde nicht gefunden !" + ex.Message + ex.StackTrace);
            }
        }*/
    }
}
