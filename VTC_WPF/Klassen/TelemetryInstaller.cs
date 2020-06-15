using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using VdfParser;
using System.Threading.Tasks;

namespace VTCManager.Klassen
{
    class TelemetryInstaller
    {
        //ETS
        private static string ETS2Folder64 = @"win_x64\";
        private static string ETS2Folder86 = @"win_x86\";
        //private static string ETS2_EXE = "eurotrucks2.exe";
        //STEAM
        private static string SteamLibraryConfigFile = @"\steamapps\libraryfolders.vdf";

        public static void install()
        {
            Console.WriteLine("install");
            //detect SteamInstallPath
            String SteamInstallPath = RegistryHandler.Globalread(RegistryHandler.Steam64bitRegistry, RegistryHandler.SteamInstallPathValueName).ToString();
            if (!string.IsNullOrEmpty(SteamInstallPath))
            {
                Console.WriteLine("install2");
                if (!File.Exists(SteamInstallPath+ @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder86 + "eurotrucks2.exe") && !File.Exists(SteamInstallPath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder64 + "eurotrucks2.exe"))
                {
                    Console.WriteLine("install3");
                    //if ETS2 is not found in normal Steam lib folder
                    String SteamLibraryConfigPath = SteamInstallPath + SteamLibraryConfigFile;
                    if (File.Exists(SteamLibraryConfigPath))
                    {
                        Console.WriteLine("install4");
                        string testFile = File.ReadAllText(SteamLibraryConfigPath);
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
                                Console.WriteLine("install5");
                                //create plugins folder if necessary and copy dll
                                if (!Directory.Exists(Steampath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder64 + "plugins"))
                                    Directory.CreateDirectory(Steampath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder64 + "plugins");
                                if (File.Exists(Steampath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder64 + @"plugins\scs-telemetry.dll"))
                                    File.Delete(Steampath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder64 + @"plugins\scs-telemetry.dll");
                                File.Copy(@"Resources/scs-telemetry.dll", Steampath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder64 + @"plugins\scs-telemetry.dll");
                                if (!Directory.Exists(Steampath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder86 + "plugins"))
                                    Directory.CreateDirectory(Steampath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder86 + "plugins");
                                if (File.Exists(Steampath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder86 + @"plugins\scs-telemetry.dll"))
                                    File.Delete(Steampath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder86 + @"plugins\scs-telemetry.dll");
                                File.Copy(@"Resources/scs-telemetry.dll", Steampath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder86 + @"plugins\scs-telemetry.dll");
                                RegistryHandler.write("ETS2Path", Steampath + @"\steamapps\common\Euro Truck Simulator 2\", "Telemetry");
                                RegistryHandler.write("Version", Config.TelemetryVersion, "Telemetry");
                                Console.WriteLine("reg geschrieben");
                            }
                        }
                    }
                    else
                    {
                        ETS2PathDialog pathwindow = new ETS2PathDialog();
                        pathwindow.Show();
                    }
                }
                else
                {
                    Console.WriteLine("install6");
                    //ETS2 found in normal Steam folder
                    //create plugins folder if necessary and copy dll
                    if (!Directory.Exists(SteamInstallPath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder64 + "plugins"))
                        Directory.CreateDirectory(SteamInstallPath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder64 + "plugins");
                    if (File.Exists(SteamInstallPath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder64 + @"plugins\scs-telemetry.dll"))
                        File.Delete(SteamInstallPath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder64 + @"plugins\scs-telemetry.dll");
                    File.Copy(@"Resources/scs-telemetry.dll", SteamInstallPath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder64 + @"plugins\scs-telemetry.dll");
                    if (!Directory.Exists(SteamInstallPath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder86 + "plugins"))
                        Directory.CreateDirectory(SteamInstallPath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder86 + "plugins");
                    if (File.Exists(SteamInstallPath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder86 + @"plugins\scs-telemetry.dll"))
                        File.Delete(SteamInstallPath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder86 + @"plugins\scs-telemetry.dll");
                    File.Copy(@"Resources/scs-telemetry.dll", SteamInstallPath + @"\steamapps\common\Euro Truck Simulator 2\bin\" + ETS2Folder86 + @"plugins\scs-telemetry.dll");
                    RegistryHandler.write("ETS2Path", SteamInstallPath + @"\steamapps\common\Euro Truck Simulator 2\", "Telemetry");
                    RegistryHandler.write("Version", Config.TelemetryVersion, "Telemetry");
                }
            }
            else
            {
                ETS2PathDialog pathwindow = new ETS2PathDialog();
                pathwindow.Show();
            }
        }

        public static void check()
        {
            String telemetryVersion = "";
            String telemetryETS2Path = "";
            try
            {
                telemetryETS2Path = RegistryHandler.read("Telemetry", "ETS2Path").ToString();
            } catch {}
            try
            {
                telemetryVersion = RegistryHandler.read("Telemetry", "Version").ToString();
            }
            catch {}

            if (String.IsNullOrWhiteSpace(telemetryETS2Path) || String.IsNullOrWhiteSpace(telemetryVersion))
            {
                Console.WriteLine("running installation of Telemetry");
                install();
            }
            else if(RegistryHandler.read("Telemetry", "Version").ToString() != Config.TelemetryVersion || !File.Exists(RegistryHandler.read("Telemetry", "ETS2Path").ToString() + @"bin\" + ETS2Folder86 + "eurotrucks2.exe") || !File.Exists(RegistryHandler.read("Telemetry", "ETS2Path").ToString() + @"bin\" + ETS2Folder64 + "eurotrucks2.exe")){
                Console.WriteLine("running installation of Telemetry2");
                install();
            }
            else if (!File.Exists(RegistryHandler.read("Telemetry", "ETS2Path").ToString() + @"bin\" + ETS2Folder86 + @"plugins\scs-telemetry.dll") || !File.Exists(RegistryHandler.read("Telemetry", "ETS2Path").ToString() + @"bin\" + ETS2Folder64 + @"plugins\scs-telemetry.dll"))
            {
                Console.WriteLine("running installation of Telemetry3");
                install();
            }

        }


    }
}
