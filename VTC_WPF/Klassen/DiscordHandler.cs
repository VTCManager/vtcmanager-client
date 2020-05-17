using DiscordRPC;

namespace VTC_WPF.Klassen
{
    internal class DiscordHandler
    {
        private class Discord
        {
            private DiscordRpcClient client;

            public Discord()
            {

                client = new DiscordRpcClient(Config.DiscordAppID);
                client.Initialize();
                client.SetPresence(new RichPresence()
                {
                    Details = "Starte...",
                    Assets = new Assets()
                    {
                        LargeImageKey = Config.DiscordLargeImageKey,
                        LargeImageText = "Beyond the limits",
                        SmallImageKey = Config.DiscordSmallImageKey,
                        SmallImageText = "VTCManager Version " + Config.ClientVersion
                    }

                });
            }
        }
    }
}
