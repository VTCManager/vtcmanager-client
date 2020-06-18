using DiscordRPC;
using DiscordRPC.Logging;
using System;
using System.Timers;

namespace VTCManager.Klassen
{
    public static class DiscordHandler
    {
        private static DiscordRpcClient client;
        private const string DiscordSmallImageKey = "vtcm-logo-orig";
        private const string DiscordAppID = "659036297561767948";
        private const string DefaultDiscordLargeImageKey = "truck-icon";
        private static Timer onJobInfoTimer;
        public static Timer FreeRoamSpeedUpdateTimer;
        private static RichPresence jobRPC;
        private static bool JobInfoActive;
        private static Translation translation;

        public static void init(Translation translation)
        {
            FreeRoamSpeedUpdateTimer = new Timer();
            onJobInfoTimer = new Timer();
            DiscordHandler.translation = translation;
            client = new DiscordRpcClient(DiscordAppID);
            client.Logger = new ConsoleLogger() { Level = LogLevel.Info };
            client.Initialize();

            client.OnPresenceUpdate += (sender, e) =>
            {
                Console.WriteLine("Received Update! {0}", e.Presence);
            };

            //FREEROAM INIT
            FreeRoamSpeedUpdateTimer.Elapsed += new ElapsedEventHandler(updateFreeRoamSpeed);
            FreeRoamSpeedUpdateTimer.Interval = 3000;
            //JOBINFO INIT
            onJobInfoTimer.Elapsed += new ElapsedEventHandler(updateOnJob);
            onJobInfoTimer.Interval = 5000;
        }

        public static void OnJobStart(string cargo, int weight, string origin, string destination, string truck_brand_id, string truck)
        {
            Console.WriteLine("job start");
            FreeRoamSpeedUpdateTimer.Stop();
            TelemetryHandler.freeroamactive = false;
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
            JobInfoActive = true;
            onJobInfoTimer.Start();
        }

        private static void updateOnJob(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine(sender);
            if (JobInfoActive)
            {
                Console.WriteLine("job update 1");
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
                Console.WriteLine("job update2");
                client.SetPresence(jobRPC);
                JobInfoActive = true;
            }
        }
        private static void updateFreeRoamSpeed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("free roam update");
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
                JobInfoActive = false;
        }

        public static void FreeRoam()
        {
            Console.WriteLine("free roam");
            onJobInfoTimer.Stop();
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
            FreeRoamSpeedUpdateTimer.Start();
        }

        private static string getTruckImageKey(string truck_brand_id)
        {
            string DiscordLargeImageKey = DefaultDiscordLargeImageKey;
            if (truck_brand_id == "renault")
            {
                DiscordLargeImageKey = "brand-renault";
            }
            else if (truck_brand_id == "scania")
            {
                DiscordLargeImageKey = "brand-scania";
            }
            else if (truck_brand_id == "mercedes")
            {
                DiscordLargeImageKey = "brand-mercedes";
            }
            else if (truck_brand_id == "volvo")
            {
                DiscordLargeImageKey = "brand-volvo";
            }
            return DiscordLargeImageKey;
        }
        public static void idle()
        {
            Console.WriteLine("idle");
            FreeRoamSpeedUpdateTimer.Stop();
            onJobInfoTimer.Stop();
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
