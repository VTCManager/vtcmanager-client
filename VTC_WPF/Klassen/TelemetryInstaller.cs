using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using VdfParser;

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
                if (!File.Exists(SteamInstallPath+ @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder86 + "eurotrucks2.exe") && File.Exists(SteamInstallPath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder64 + "eurotrucks2.exe"))
                {
                    //if ETS2 is not found in normal Steam lib folder
                    String SteamLibraryConfigPath = SteamInstallPath + SteamLibraryConfigFile;
                    if (File.Exists(SteamLibraryConfigPath))
                    {
                        string testFile = File.ReadAllText(SteamLibraryConfigPath);
                        Console.WriteLine(testFile);
                        VdfDeserializer deserializer = new VdfDeserializer();
                        dynamic result = deserializer.Deserialize(testFile);
                        IDictionary<string, object> result_dictionary = result.LibraryFolders;
                        List<string> SteamLibraries = new List<string>();
                        for (int i = 1; i < 5; i++)
                        {
                            if (result_dictionary.ContainsKey(i.ToString()))
                            {
                                SteamLibraries.Add(result_dictionary[i.ToString()].ToString());
                            }
                        }
                        foreach (String Steampath in SteamLibraries)
                        {
                            if (File.Exists(Steampath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder86 + "eurotrucks2.exe") && File.Exists(Steampath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder64 + "eurotrucks2.exe"))
                            {
                                //create plugins folder if necessary and copy dll
                                if (!Directory.Exists(Steampath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder64 + "plugins"))
                                    Directory.CreateDirectory(Steampath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder64 + "plugins");
                                File.Copy(@"Resources/scs-telemetry.dll", Steampath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder64 + @"plugins\scs-telemetry.dll");
                                if (!Directory.Exists(Steampath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder86 + "plugins"))
                                    Directory.CreateDirectory(Steampath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder86 + "plugins");
                                File.Copy(@"Resources/scs-telemetry.dll", Steampath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder86 + @"plugins\scs-telemetry.dll");
                            }
                        }
                    }
                    else
                    {
                        //ETS2 found in normal Steam folder
                        //create plugins folder if necessary and copy dll
                        if (!Directory.Exists(SteamInstallPath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder64 + "plugins"))
                            Directory.CreateDirectory(SteamInstallPath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder64 + "plugins");
                        File.Copy(@"Resources/scs-telemetry.dll", SteamInstallPath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder64 + @"plugins\scs-telemetry.dll");
                        if (!Directory.Exists(SteamInstallPath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder86 + "plugins"))
                            Directory.CreateDirectory(SteamInstallPath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder86 + "plugins");
                        File.Copy(@"Resources/scs-telemetry.dll", SteamInstallPath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder86 + @"plugins\scs-telemetry.dll");
                    }
                }
            }
            /*
            String TelemetryPath = RegistryHandler.Globalread(Config.ETS2InstallFolderRegistryEntry, Config.ETS2InstallFolderRegistryEntryValue).ToString();
            if (!string.IsNullOrWhiteSpace(TelemetryPath))
            {
                TelemetryPath = TelemetryPath.Replace(ETS2_EXE, "");
                //remove the folder containing the exe from the path (both options can be used so I use 64 AND 86)
                TelemetryPath = TelemetryPath.Replace(ETS2Folder64, "");
                TelemetryPath = TelemetryPath.Replace(ETS2Folder86, "");
                File.Copy(@"Resources/scs-telemetry.dll", TelemetryPath + ETS2Folder64 + @"plugins\scs-telemetry.dll");
                File.Copy(@"Resources/scs-telemetry.dll", TelemetryPath + ETS2Folder86 + @"plugins\scs-telemetry.dll");
            }*/
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
