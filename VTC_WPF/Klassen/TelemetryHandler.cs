﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace VTC_WPF.Klassen
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

        }

        public static void JobCancelled(object sender, EventArgs e)
        {

        }

        public static void JobStarted(object sender, EventArgs e)
        {
            while (Telemetry_Data == null)
            {
                Console.WriteLine("wait");
            }
            Console.WriteLine("start");
            Console.WriteLine(Telemetry_Data.JobValues.CityDestination);
            Console.WriteLine(API.jobStarted);
            Dictionary<string, string> post_param = new Dictionary<string, string>();
            post_param.Add("client_ident", Config.macAddr);
            post_param.Add("origin", Telemetry_Data.JobValues.CitySource);
            post_param.Add("destination", Telemetry_Data.JobValues.CityDestination);
            post_param.Add("cargo", Telemetry_Data.JobValues.CargoValues.Name);
            post_param.Add("cargo_weight", Telemetry_Data.JobValues.CargoValues.Mass.ToString());
            post_param.Add("planned_distance", Telemetry_Data.JobValues.PlannedDistanceKm.ToString());
            Console.WriteLine(API.HTTPSRequestPost(API.jobStarted, post_param));
        }
    }
}
