using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordRPC;

namespace VTC_WPF.Klassen
{
    class DiscordHandler
    {
        class Discord
        {
            private DiscordRpcClient client;

            public Discord()
            {

                     client = new DiscordRpcClient(Information.DiscordAppID);
                        client.Initialize();
                        client.SetPresence(new RichPresence()
                        {
                            Details = "Starte...",
                            Assets = new Assets()
                            {
                                LargeImageKey = Information.DiscordLargeImageKey,
                                LargeImageText = "Beyond the limits",
                                SmallImageKey = Information.DiscordSmallImageKey,
                                SmallImageText = "VTCManager Version " + Information.ClientVersion
                            }

                        });
            }
        }
    }
}
