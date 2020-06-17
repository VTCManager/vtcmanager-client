using DiscordRPC;
using System;
using System.Timers;

namespace VTCManager.Klassen
{
    public class DiscordHandler
    {
        private DiscordRpcClient client;
        private const string DiscordSmallImageKey = "vtcm-logo-orig";
        private const string DiscordAppID = "659036297561767948";
        private const string DefaultDiscordLargeImageKey = "truck-icon";
        private static Timer onJobInfoTimer;
        private static RichPresence jobRPC;
        private static bool JobInfoActive;

        public DiscordHandler(Translation translation)
        {

            client = new DiscordRpcClient(DiscordAppID);
            client.Initialize();
            client.SetPresence(new RichPresence()
            {
                Details = "Idle",
                Assets = new Assets()
                {
                    LargeImageKey = DefaultDiscordLargeImageKey,
                    LargeImageText = "Beyond the limits",
                    SmallImageKey = DiscordSmallImageKey,
                    SmallImageText = "v" + Config.ClientVersion
                }

            });
        }

        public void OnJobStart(string cargo, int weight, string origin, string destination, string truck_brand_id, string truck)
        {
            string DiscordLargeImageKey = getTruckImageKey(truck_brand_id);
            jobRPC = new RichPresence()
            {
                Details = "Fracht: " + cargo + "(" + weight/1000 + "t)",
                State = "von " + origin + " nach " + destination,

                Assets = new Assets()
                {
                    LargeImageKey = DiscordLargeImageKey,
                    LargeImageText = "Fährt "+ truck,
                    SmallImageKey = DiscordSmallImageKey,
                    SmallImageText = "v" + Config.ClientVersion
                }
            };
            jobRPC = jobRPC.WithTimestamps(Timestamps.Now);
            client.SetPresence(jobRPC);
            client.Invoke();
            JobInfoActive = true;
            onJobInfoTimer = new Timer();
            onJobInfoTimer.Elapsed += new ElapsedEventHandler(updateOnJob);
            onJobInfoTimer.Interval = 5000;
            onJobInfoTimer.Enabled = true;
        }

        private void updateOnJob(object sender, ElapsedEventArgs e)
        {
            if (JobInfoActive)
            {
                RichPresence RPC = new RichPresence()
                {
                    Details = (int)TelemetryHandler.Telemetry_Data.TruckValues.CurrentValues.DashboardValues.Speed.Kph+" km/h",
                    State = (int)TelemetryHandler.Telemetry_Data.NavigationValues.NavigationDistance/1000+" km("+ (int)((((TelemetryHandler.Telemetry_Data.NavigationValues.NavigationDistance/1000)/TelemetryHandler.Telemetry_Data.JobValues.PlannedDistanceKm)*100)) + "%) verbleibend",

                    Assets = new Assets()
                    {
                        LargeImageKey = jobRPC.Assets.LargeImageKey,
                        LargeImageText = jobRPC.Assets.LargeImageText,
                        SmallImageKey = jobRPC.Assets.SmallImageKey,
                        SmallImageText = jobRPC.Assets.SmallImageText
                    }
                };
                client.SetPresence(RPC);
                client.Invoke();
                JobInfoActive = false;
            }
            else
            {
                client.SetPresence(jobRPC);
                client.Invoke();
                JobInfoActive = true;
            }
        }

        public void FreeRoam(string truck_brand_id, string truck)
        {
            string DiscordLargeImageKey = getTruckImageKey(truck_brand_id);
            RichPresence RPC = new RichPresence()
            {
                Details = "Frei wie der Wind",

                Assets = new Assets()
                {
                    LargeImageKey = DiscordLargeImageKey,
                    LargeImageText = "Driving " + truck,
                    SmallImageKey = DiscordSmallImageKey,
                    SmallImageText = "v" + Config.ClientVersion
                }
            };
            client.SetPresence(RPC);
            client.Invoke();
        }

        private string getTruckImageKey(string truck_brand_id)
        {
            string DiscordLargeImageKey = DefaultDiscordLargeImageKey;
            if (truck_brand_id == "renault")
            {
                DiscordLargeImageKey = "brand-renault";
            }
            return DiscordLargeImageKey;
        }
    }
}
