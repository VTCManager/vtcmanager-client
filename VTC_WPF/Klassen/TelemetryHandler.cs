using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace VTC_WPF.Klassen
{
    public class TelemetryHandler
    {
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

        }
    }
}
