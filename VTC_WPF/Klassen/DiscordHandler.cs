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
        public static Timer FreeRoamSpeedUpdateTimer;
        private static RichPresence jobRPC;
        private static bool JobInfoActive;
        private static Translation translation;

        public DiscordHandler(Translation translation)
        {
            FreeRoamSpeedUpdateTimer = new Timer();
            onJobInfoTimer = new Timer();
            DiscordHandler.translation = translation;
            client = new DiscordRpcClient(DiscordAppID);
            client.Initialize();
            client.SetPresence(new RichPresence()
            {
                Details = translation.DISCORD_IDLE,
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
            FreeRoamSpeedUpdateTimer.Enabled = false;
            string DiscordLargeImageKey = getTruckImageKey(truck_brand_id);
            jobRPC = new RichPresence()
            {
                Details = translation.DISCORD_JOB_CARGO+": " + cargo + "(" + weight/1000 + "t)",
                State = origin + "->" + destination,

                Assets = new Assets()
                {
                    LargeImageKey = DiscordLargeImageKey,
                    LargeImageText = translation.DISCORD_DRIVING + " " + truck,
                    SmallImageKey = DiscordSmallImageKey,
                    SmallImageText = "v" + Config.ClientVersion
                }
            };
            jobRPC = jobRPC.WithTimestamps(Timestamps.Now);
            client.SetPresence(jobRPC);
            client.Invoke();
            JobInfoActive = true;
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
                    State = (int)TelemetryHandler.Telemetry_Data.NavigationValues.NavigationDistance/1000+" km("+ (int)((((TelemetryHandler.Telemetry_Data.NavigationValues.NavigationDistance/1000)/TelemetryHandler.Telemetry_Data.JobValues.PlannedDistanceKm)*100)) + "%) "+translation.DISCORD_JOB_REMAINING,

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
        private void updateFreeRoamSpeed(object sender, ElapsedEventArgs e)
        {
            string DiscordLargeImageKey = getTruckImageKey(TelemetryHandler.Telemetry_Data.TruckValues.ConstantsValues.BrandId);
            RichPresence RPC = new RichPresence()
                {
                    Details = translation.DISCORD_FREEROAM,
                    State = (int)TelemetryHandler.Telemetry_Data.TruckValues.CurrentValues.DashboardValues.Speed.Kph + " km/h",

                    Assets = new Assets()
                    {
                        LargeImageKey = DiscordLargeImageKey,
                        LargeImageText = translation.DISCORD_DRIVING + " " + TelemetryHandler.Telemetry_Data.TruckValues.ConstantsValues.Brand+" "+ TelemetryHandler.Telemetry_Data.TruckValues.ConstantsValues.Name,
                        SmallImageKey = DiscordSmallImageKey,
                        SmallImageText = "v" + Config.ClientVersion
                    }
                };
                client.SetPresence(RPC);
                client.Invoke();
                JobInfoActive = false;
        }

        public void FreeRoam()
        {
            onJobInfoTimer.Enabled = false;
            string DiscordLargeImageKey = getTruckImageKey(TelemetryHandler.Telemetry_Data.TruckValues.ConstantsValues.BrandId);
            RichPresence RPC = new RichPresence()
            {
                Details = translation.DISCORD_FREEROAM,
                State = (int)TelemetryHandler.Telemetry_Data.TruckValues.CurrentValues.DashboardValues.Speed.Kph + " km/h",

                Assets = new Assets()
                {
                    LargeImageKey = DiscordLargeImageKey,
                    LargeImageText = translation.DISCORD_DRIVING + " " + TelemetryHandler.Telemetry_Data.TruckValues.ConstantsValues.Brand + " " + TelemetryHandler.Telemetry_Data.TruckValues.ConstantsValues.Name,
                    SmallImageKey = DiscordSmallImageKey,
                    SmallImageText = "v" + Config.ClientVersion
                }
            };
            client.SetPresence(RPC);
            client.Invoke();
            FreeRoamSpeedUpdateTimer.Elapsed += new ElapsedEventHandler(updateFreeRoamSpeed);
            FreeRoamSpeedUpdateTimer.Interval = 3000;
            FreeRoamSpeedUpdateTimer.Enabled = true;
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
        public void idle()
        {
            client = new DiscordRpcClient(DiscordAppID);
            client.Initialize();
            client.SetPresence(new RichPresence()
            {
                Details = translation.DISCORD_IDLE,
                Assets = new Assets()
                {
                    LargeImageKey = DefaultDiscordLargeImageKey,
                    LargeImageText = "Beyond the limits",
                    SmallImageKey = DiscordSmallImageKey,
                    SmallImageText = "v" + Config.ClientVersion
                }

            });
        }
    }
}
