using Newtonsoft.Json.Linq;
using SCSSdkClient;
using SCSSdkClient.Object;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using System.Windows;

namespace VTCManager.Klassen
{
    public class TelemetryHandler
    {
        public static SCSTelemetry Telemetry_Data;
        public SCSSdkTelemetry Telemetry;
        private MainWindow mainWindow;
        public static DiscordHandler Discord;
        public static bool onJob = false;

        public TelemetryHandler(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            Discord = new DiscordHandler();
            Telemetry = new SCSSdkTelemetry();
            Telemetry.Data += Telemetry_Data_Handler;
            Telemetry.JobStarted += TelemetryHandler.JobStarted;
            Telemetry.JobCancelled += TelemetryHandler.JobCancelled;
            Telemetry.JobDelivered += TelemetryHandler.JobDelivered;
            Telemetry.Fined += TelemetryHandler.Fined;
            Telemetry.Tollgate += TelemetryHandler.Tollgate;
            Telemetry.Ferry += TelemetryHandler.FerryUsed;
            Telemetry.Train += TelemetryHandler.TrainUsed;
            Telemetry.RefuelStart += TelemetryHandler.RefuelStart;
            Telemetry.RefuelEnd += TelemetryHandler.RefuelEnd;
            Telemetry.RefuelPayed += TelemetryHandler.RefuelPayed;
        }

        private void Telemetry_Data_Handler(SCSTelemetry data, bool newTimestamp)
        {
            Telemetry_Data = data;
            if(data != null)
            {
                if (!String.IsNullOrWhiteSpace(data.TruckValues.ConstantsValues.BrandId) && !onJob)
                {
                    Discord.FreeRoam(Telemetry_Data.TruckValues.ConstantsValues.BrandId, Telemetry_Data.TruckValues.ConstantsValues.Brand + " " + Telemetry_Data.TruckValues.ConstantsValues.Name);
                }
            }
        }

        public static bool IsETSRunning()
        {
            string procName = Process.GetCurrentProcess().ProcessName;    
            Process[] processes = Process.GetProcessesByName("Euro Truck Simulator 2");

            if (processes.Length > 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static void RefuelStart(object sender, EventArgs e)
        {

        }

        public static void RefuelPayed(object sender, EventArgs e)
        {

        }

        public static void RefuelEnd(object sender, EventArgs e)
        {

        }

        public static void TrainUsed(object sender, EventArgs e)
        {

        }

        public static void FerryUsed(object sender, EventArgs e)
        {

        }

        public static void Fined(object sender, EventArgs e)
        {

        }

        public static void Tollgate(object sender, EventArgs e)
        {

        }

        public static void JobDelivered(object sender, EventArgs e)
        {
            checkTelemetry();
            Dictionary<string, string> post_param = new Dictionary<string, string>();
            post_param.Add("delivered_time", Telemetry_Data.CommonValues.GameTime.Date.ToString());
            post_param.Add("cargo_damage", Telemetry_Data.JobValues.CargoValues.CargoDamage.ToString());
            post_param.Add("fuel_at_end", Telemetry_Data.TruckValues.CurrentValues.DashboardValues.FuelValue.Amount.ToString());
            post_param.Add("truck_damage_at_end", Telemetry_Data.TruckValues.CurrentValues.DamageValues.Engine.ToString());
            JObject response = API.HTTPSRequestPost(API.job_delivered, post_param);
            onJob = false;
        }

        public static void JobCancelled(object sender, EventArgs e)
        {
            onJob = false;
        }

        public static void JobStarted(object sender, EventArgs e)
        {
            checkTelemetry();
            if (String.IsNullOrWhiteSpace(Telemetry_Data.JobValues.CitySource))
            {
                Stopwatch stopWatch = new Stopwatch(); //as timeout
                stopWatch.Start();
                while (String.IsNullOrWhiteSpace(Telemetry_Data.JobValues.CitySource) && stopWatch.ElapsedMilliseconds < 5000)
                {
                    Console.WriteLine("Waiting for tour data to init");
                }
                stopWatch.Stop();
                Console.WriteLine("Wait for data took "+ stopWatch.ElapsedMilliseconds+" ms");
            }
            Dictionary<string, string> post_param = new Dictionary<string, string>();
            post_param.Add("origin", Telemetry_Data.JobValues.CitySource);
            post_param.Add("origin_id", Telemetry_Data.JobValues.CitySourceId);
            post_param.Add("origin_company", Telemetry_Data.JobValues.CompanySource);
            post_param.Add("origin_company_id", Telemetry_Data.JobValues.CompanySourceId);
            post_param.Add("destination", Telemetry_Data.JobValues.CityDestination);
            post_param.Add("destination_id", Telemetry_Data.JobValues.CityDestinationId);
            post_param.Add("destination_company", Telemetry_Data.JobValues.CompanyDestination);
            post_param.Add("destination_company_id", Telemetry_Data.JobValues.CompanyDestinationId);
            post_param.Add("cargo", Telemetry_Data.JobValues.CargoValues.Name);
            post_param.Add("cargo_id", Telemetry_Data.JobValues.CargoValues.Id);
            post_param.Add("cargo_weight", Telemetry_Data.JobValues.CargoValues.Mass.ToString());
            post_param.Add("planned_distance", Telemetry_Data.JobValues.PlannedDistanceKm.ToString());
            post_param.Add("ets_income", Telemetry_Data.JobValues.Income.ToString());
            post_param.Add("delivery_deadline", Telemetry_Data.JobValues.DeliveryTime.Date.ToString());
            post_param.Add("freight_market", Telemetry_Data.JobValues.Market.ToString());
            post_param.Add("fuel_at_beginning", Telemetry_Data.TruckValues.CurrentValues.DashboardValues.FuelValue.Amount.ToString());
            post_param.Add("truck_damage_at_beginning", Telemetry_Data.TruckValues.CurrentValues.DamageValues.Engine.ToString());
            post_param.Add("truck_id", Telemetry_Data.TruckValues.ConstantsValues.Id);
            post_param.Add("truck", Telemetry_Data.TruckValues.ConstantsValues.Name);
            post_param.Add("truck_brand_id", Telemetry_Data.TruckValues.ConstantsValues.BrandId);
            post_param.Add("truck_brand", Telemetry_Data.TruckValues.ConstantsValues.Brand);
            JObject response = API.HTTPSRequestPost(API.job_started, post_param);
            Discord.OnJobStart(Telemetry_Data.JobValues.CargoValues.Name, (int)Telemetry_Data.JobValues.CargoValues.Mass, Telemetry_Data.JobValues.CitySource, Telemetry_Data.JobValues.CityDestination, Telemetry_Data.TruckValues.ConstantsValues.BrandId, Telemetry_Data.TruckValues.ConstantsValues.Brand+" "+ Telemetry_Data.TruckValues.ConstantsValues.Name);
            onJob = true;
        }

        private static void checkTelemetry()
        {
            //bug fix when an event occures while booting the app -> Telemetry_Data is null
            while (Telemetry_Data == null)
            {
                Console.WriteLine("Waiting for init to be finished");
            }
        }
    }
}
