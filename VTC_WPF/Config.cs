namespace VTC_WPF
{
    public static class Config
    {
        //Versions
        public static string ClientVersion = "3.0.0 DevAlpha";

        //DISCORD
        public static string DiscordAppID = "659036297561767948";
        public static string DiscordLargeImageKey = "truck-icon";
        public static string DiscordSmallImageKey = "vtcm-logo";

        //FILES
        public static string JobsCache = "JobsCache.json";

        //REGISTRY ENTRIES
        public static string ETS2InstallFolderRegistryEntry = @"HKEY_CURRENT_USER\System\GameConfigStore\Children\03f2c1c8-e376-4256-93ff-d7dfded8044e";
        public static string ETS2InstallFolderRegistryEntryValue = "MatchedExeFullPath";
    }
}
