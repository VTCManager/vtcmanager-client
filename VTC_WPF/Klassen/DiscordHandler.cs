using DiscordRPC;

namespace VTC_WPF.Klassen
{
    internal class DiscordHandler
    {
        private DiscordRpcClient client;

        public DiscordHandler()
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
                    SmallImageText = "v" + Config.ClientVersion
                }

            });
        }
    }
}
