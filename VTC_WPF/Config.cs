using System;

namespace VTCManager
{
    public static class Config
    {
        //VERSIONS
        public static string ClientVersion = "3.0.0 DevAlpha";
        public static string TelemetryVersion = "1.10";

        //DISCORD
        public static string DiscordAppID = "659036297561767948";
        public static string DiscordLargeImageKey = "truck-icon";
        public static string DiscordSmallImageKey = "vtcm-logo";

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
