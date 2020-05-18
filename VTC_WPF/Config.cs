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
        public static string ETS2InstallFolderRegistryEntry = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
        public static string ETS2InstallFolderRegistryEntryValue = "InstallLocation";
    }
}
