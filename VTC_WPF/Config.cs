using System;

namespace VTCManager
{
    public static class Config
    {
        //VERSIONS
        public static string ClientVersion = "3.0.3.2 Beta";
        public static string TelemetryVersion = "1.10";

        //FILES
        public static string JobsCache = "JobsCache.json";

        //REGISTRY ENTRIES
        public static string ETS2InstallFolderRegistryEntry = @"HKEY_CURRENT_USER\System\GameConfigStore\Children\03f2c1c8-e376-4256-93ff-d7dfded8044e";
        public static string ETS2InstallFolderRegistryEntryValue = "MatchedExeFullPath";

        // LOGGING
        public static string LogRoot = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\VTCManager";
        public static string ClientLogFileName = @"\Client_Log.txt";
        public static string SystemLogFileName = @"\System_Log.txt";

        //TOKENS
        public static string AccessToken = ""; //set in the login process

    }
}
