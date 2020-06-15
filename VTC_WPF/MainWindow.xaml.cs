﻿using Microsoft.Win32;
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
using ControlzEx.Theming;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;

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
            Logging.Make_Log_File(); 
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

            Lade_Themes();
            utils.Build_Registry();
            Translation Translation = new Translation(utils.Reg_Lesen("Config", "Sprache", false));

            Sprachauswahl.SelectedValue = utils.Reg_Lesen("Config", "Sprache", false);

            TMP_Pfad_Textbox.Text = utils.Reg_Lesen("Config", "TMP_PFAD", true);
            Sprachauswahl.SelectedItem = utils.Reg_Lesen("Config", "Sprache", false);
            TMP_Pfad_Textbox.IsEnabled = string.IsNullOrEmpty(utils.Reg_Lesen("Config", "TMP_PFAD", true)) ? true : false;

            LBL_TRUCK.Content = Translation.truck_lb;

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
                    Truck_Daten.RPM = data.TruckValues.CurrentValues.DashboardValues.RPM.ToString();

                    // Schadensanzeige
                    Truck_Daten.TRUCK_MOTOR_SCHADEN = Convert.ToInt32(data.TruckValues.CurrentValues.DamageValues.Engine * 100);
                    Truck_Daten.TRUCK_GETRIEBE_SCHADEN = Convert.ToInt32(data.TruckValues.CurrentValues.DamageValues.Transmission * 100);
                    Truck_Daten.TRUCK_FAHRWERK_SCHADEN = Convert.ToInt32(data.TruckValues.CurrentValues.DamageValues.Chassis * 100);
                    Truck_Daten.TRUCK_CHASSIS_SCHADEN = Convert.ToInt32(data.TruckValues.CurrentValues.DamageValues.Cabin * 100);
                    Truck_Daten.TRUCK_RAEDER_SCHADEN = Convert.ToInt32(data.TruckValues.CurrentValues.DamageValues.WheelsAvg * 100);
                    Truck_Daten.TRAILER_FRACHT_SCHADEN = Convert.ToInt32(data.JobValues.CargoValues.CargoDamage*100);
                    // Definition ETS / ATS
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
            TMP_Pfad_Textbox.IsEnabled = false;
        }

        private void btn_open_suche_ets_pfad(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open_ETS2_Path = new OpenFileDialog();
            if (open_ETS2_Path.ShowDialog() == true)
                ETS2_Pfad_Textbox.Text = open_ETS2_Path.FileName.ToString(); utils.Reg_Schreiben("ETS2_PFAD", open_ETS2_Path.FileName.ToString(), "Config");
            ETS2_Pfad_Textbox.IsEnabled = false;
        }

        private void btn_open_suche_ats_pfad(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open_ATS_Path = new OpenFileDialog();
            if (open_ATS_Path.ShowDialog() == true)
                ATS_Pfad_Textbox.Text = open_ATS_Path.FileName.ToString(); utils.Reg_Schreiben("ATS_PFAD", open_ATS_Path.FileName.ToString(), "Config");
            ATS_Pfad_Textbox.IsEnabled = false;
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

        private void Designauswahl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            utils.Reg_Schreiben("Design", (string)(Designauswahl.SelectedValue), "Config");
            try
            {
                ThemeManager.Current.ChangeTheme(this, (string)(Designauswahl.SelectedValue));
            } catch (Exception ex)
            {
                Logging.WriteClientLog("Designer: Konnte das Design " + (string)(Designauswahl.SelectedValue) + " nicht laden." + ex.Message + ex.StackTrace);
            }
                

        }





        private void Launch_ATS(object sender, RoutedEventArgs e)
        {
            try
            {
                FileHandler.StarteAnwendung(utils.Reg_Lesen("Telemetry", "ETS2PathDialog", true));
            } catch
            {
                Show_Message("Leider ist der Link noch nicht Implementiert ! Dieser wird in einem der nächten Updates folgen...");
            }
            
        }

        private void Launch_ETS(object sender, RoutedEventArgs e)
        {
            Show_Message("Leider ist der Link noch nicht Implementiert ! Dieser wird in einem der nächten Updates folgen...");
        }




        public async void Show_Message(string text)
        {
            await this.ShowMessageAsync(text, "Das Team von VTC-Manager.");
        }

        private void Lade_Themes()
        {
            if (string.IsNullOrEmpty(utils.Reg_Lesen("Config", "Design", false)))
            {
                utils.Reg_Schreiben("Design", "Dark.Blue", "Config");
                ThemeManager.Current.ChangeTheme(this, "Dark.Blue");
            }
            else
            {
                Designauswahl.SelectedValue = utils.Reg_Lesen("Config", "Design", false);
                ThemeManager.Current.ChangeTheme(this, utils.Reg_Lesen("Config", "Design", false));
            }
        }

        private void Sprachauswahl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string sprache = (string)Sprachauswahl.SelectedValue;

            if (utils.Reg_Lesen("Config", "Sprache", false) != (string)Sprachauswahl.SelectedValue)
            {
                Show_Message("Die Sprache wird beim nächsten Systemstart geändert !");
            }

            utils.Reg_Schreiben("Sprache", sprache, "Config");
            Translation Translation = new Translation(utils.Reg_Lesen("Config", "Design", false));



        }
    }
}
