using Microsoft.Win32;
using SCSSdkClient;
using SCSSdkClient.Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using VTCManager.Klassen;
using MahApps.Metro.Controls;

namespace VTCManager
{
    public partial class MainWindow : MetroWindow
    {
        public SCSSdkTelemetry Telemetry;
        private readonly bool InvokeRequired;
        private delegate void UpdateProgressDelegate(DependencyProperty dp, object value);
        public DiscordHandler Discord;
        public Truck_Daten Truck_Daten;
        Utilities utils = new Utilities();
        public JobHandler jobHandler;
        private OpenFileDialog tmp_Trucker;
        public int Gesch2;
        int minutes;
        int Tankinhalt;
        private object polyline1;


        public MainWindow()
        {
            Logging.Make_Log_File(); // Muss als erstes stehen, damit vor allem anderen die Logs geleert werden !
            TelemetryInstaller.check();
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
            jobHandler = new JobHandler();
            Discord = new DiscordHandler();
            Truck_Daten = new Truck_Daten();


            InitializeComponent();

            TMP_Pfad_Textbox.Text = utils.Reg_Lesen("Config", "TMP_PFAD", true);
            TMP_Pfad_Textbox.IsEnabled = string.IsNullOrEmpty(utils.Reg_Lesen("Config", "TMP_PFAD", true)) ? true : false;

            this.DataContext = Truck_Daten;
        }


        private void Telemetry_Data_Handler(SCSTelemetry data, bool updated)
        {
            try
            {
                
               

                if (!InvokeRequired)
                {



                    //set the data globally
                    TelemetryHandler.Telemetry_Data = data;

                   
                    Truck_Daten.SPEED = Convert.ToInt32(TelemetryHandler.Telemetry_Data.TruckValues.CurrentValues.DashboardValues.Speed.Kph);

                    Truck_Daten.SPEED_TACHO = Convert.ToInt32(data.TruckValues.CurrentValues.DashboardValues.Speed.Kph-85);


                    Truck_Daten.BLINKER_LINKS = data.TruckValues.CurrentValues.LightsValues.BlinkerLeftOn.ToString();
                    

                    Truck_Daten.HERSTELLER = data.TruckValues.ConstantsValues.Brand;
                    Truck_Daten.MODELL = data.TruckValues.ConstantsValues.Name;

                    Truck_Daten.FUEL_MAX = Convert.ToInt32(data.TruckValues.ConstantsValues.CapacityValues.Fuel).ToString();
                    Truck_Daten.FUEL_BESTAND = Convert.ToInt32(data.TruckValues.CurrentValues.DashboardValues.FuelValue.Amount).ToString();
                    Truck_Daten.ADBLUE_MAX = Convert.ToInt32(data.TruckValues.ConstantsValues.CapacityValues.AdBlue).ToString();
                    Truck_Daten.ADBLUE_BESTAND = Convert.ToInt32(data.TruckValues.CurrentValues.DashboardValues.AdBlue).ToString();

                    // TODO Alle Daten müssen hier an die Truck_Daten gebunden werden. Danach in der XAML mit dem Content an den VALUE binden (RPM / RAD_SCHADEN / ZIEL_FIRMA etc)
                    Truck_Daten.RPM = data.TruckValues.CurrentValues.DashboardValues.RPM.ToString();


                    // Schadensanzeige
                    Truck_Daten.TRUCK_MOTOR_SCHADEN = Convert.ToInt32(data.TruckValues.CurrentValues.DamageValues.Engine * 100);
                    Truck_Daten.TRUCK_GETRIEBE_SCHADEN = Convert.ToInt32(data.TruckValues.CurrentValues.DamageValues.Transmission * 100);
                    Truck_Daten.TRUCK_FAHRWERK_SCHADEN = Convert.ToInt32(data.TruckValues.CurrentValues.DamageValues.Chassis * 100);
                    Truck_Daten.TRUCK_CHASSIS_SCHADEN = Convert.ToInt32(data.TruckValues.CurrentValues.DamageValues.Cabin * 100);
                    Truck_Daten.TRUCK_RAEDER_SCHADEN = Convert.ToInt32(data.TruckValues.CurrentValues.DamageValues.WheelsAvg * 100);
                    
                    Truck_Daten.TRAILER_FRACHT_SCHADEN = Convert.ToInt32(data.JobValues.CargoValues.CargoDamage*100);

                    Truck_Daten.LITER_GALLONEN = (data.Game.ToString() == "Ets2") ? "Liter" : "Gall.";

                }


            }
            catch
            { }

        }


        private void btn_open_TMP_File_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                TMP_Pfad_Textbox.Text = openFileDialog.FileName.ToString(); utils.Reg_Schreiben("TMP_PFAD", openFileDialog.FileName.ToString(), "Config");
        }

        private void TMP_Starten_Click(object sender, RoutedEventArgs e)
        {
                FileHandler.StarteAnwendung(utils.Reg_Lesen("Config", "TMP_PFAD", true));
        }

        private void LaunchFaceBookSiteSite(object sender, RoutedEventArgs e)
        {

        }

        private void LaunchDiscord(object sender, RoutedEventArgs e)
        {

        }

        private void LaunchWebsite(object sender, RoutedEventArgs e)
        {
            FileHandler.StarteAnwendung("https://vtc.northwestvideo.de/");
        }
    }
}
