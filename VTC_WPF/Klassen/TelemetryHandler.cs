using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace VTCManager.Klassen
{
    public class TelemetryHandler
    {
        public static SCSSdkClient.Object.SCSTelemetry Telemetry_Data;
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
            JObject response = API.HTTPSRequestGet(API.job_delivered);
        }

        public static void JobCancelled(object sender, EventArgs e)
        {

        }

        public static void JobStarted(object sender, EventArgs e)
        {
            checkTelemetry();
            Dictionary<string, string> post_param = new Dictionary<string, string>();
            post_param.Add("origin", Telemetry_Data.JobValues.CitySource);
            post_param.Add("destination", Telemetry_Data.JobValues.CityDestination);
            post_param.Add("cargo", Telemetry_Data.JobValues.CargoValues.Name);
            post_param.Add("cargo_weight", Telemetry_Data.JobValues.CargoValues.Mass.ToString());
            post_param.Add("planned_distance", Telemetry_Data.JobValues.PlannedDistanceKm.ToString());
            post_param.Add("ets_income", Telemetry_Data.JobValues.Income.ToString());
            JObject response = API.HTTPSRequestPost(API.job_started, post_param);
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
