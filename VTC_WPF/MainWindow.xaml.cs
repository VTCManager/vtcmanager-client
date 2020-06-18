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
        private TelemetryHandler telemetryhandler;
        private Translation translation;

        public MainWindow()
        {
            Logging.Make_Log_File(); 
            TelemetryInstaller.check();
            Telemetry = new SCSSdkTelemetry();
            Telemetry.Data += Telemetry_Data_Handler;
            TelemetryInstaller.check();
            jobHandler = new JobHandler();
            Truck_Daten = new Truck_Daten();

            InitializeComponent();

            Lade_Themes();
            lade_Translations();
            //must be after lade_Translations
            telemetryhandler = new TelemetryHandler(this, translation);
            utils.Build_Registry();

            Sprachauswahl.SelectedValue = utils.Reg_Lesen("Config", "Sprache", false);

            TMP_Pfad_Textbox.Text = utils.Reg_Lesen("Config", "TMP_PFAD", true);
            Sprachauswahl.SelectedItem = utils.Reg_Lesen("Config", "Sprache", false);
            TMP_Pfad_Textbox.IsEnabled = string.IsNullOrEmpty(utils.Reg_Lesen("Config", "TMP_PFAD", true)) ? true : false;


            switch(Truck_Daten.HERSTELLER_ID)
            {
                case "mercedes":
                    BILD_ANZEIGE.Source = new BitmapImage(new Uri("Icons/icons8-mercedes-benz-256.png", UriKind.Relative));
                    break;
                case "scania":
                    BILD_ANZEIGE.Source = new BitmapImage(new Uri("Icons/icons8-scania-256.png", UriKind.Relative));
                    break;
                case "iveco":
                    BILD_ANZEIGE.Source = new BitmapImage(new Uri("Icons/icons8-iveco-256.png", UriKind.Relative));
                    break;
                case "daf":
                    BILD_ANZEIGE.Source = new BitmapImage(new Uri("Icons/icons8-iveco-256.png", UriKind.Relative));
                    break;

            }

 


            this.DataContext = Truck_Daten;
        }

     


        private void Telemetry_Data_Handler(SCSTelemetry data, bool updated)
        {
            try
            {
                if (!InvokeRequired)
                {
                    // ALLGEMEINES
                    Truck_Daten.TelemetryVersion = data.TelemetryVersion.Major.ToString() + "." + data.TelemetryVersion.Minor.ToString();
                    Truck_Daten.SPIEL_PAUSE = data.Paused;
                    Truck_Daten.SPIEL = data.Game.ToString();

                    Truck_Daten.ANZEIGE_KM_MILES = (Truck_Daten.SPIEL == "Ets2") ? " km " : " mi";
                    Truck_Daten.ANZEIGE_TO_LBS = (Truck_Daten.SPIEL == "Ets2") ? " t " : " lb ";

                    var uriSource = new Uri(@"Resourcen/icon8-mercedes-benz-256.png", UriKind.Relative);
                   
                    Truck_Daten.SPEED_KMH = (int)data.TruckValues.CurrentValues.DashboardValues.Speed.Kph;
                    Truck_Daten.SPEED_TACHO_KMH = (int)data.TruckValues.CurrentValues.DashboardValues.Speed.Kph-85;
                    Truck_Daten.SPEED_MPH = (int)data.TruckValues.CurrentValues.DashboardValues.Speed.Kph;
                    Truck_Daten.SPEED_TACHO_MPH = (int)data.TruckValues.CurrentValues.DashboardValues.Speed.Kph - 85;
                    Truck_Daten.BLINKER_LINKS = (bool)data.TruckValues.CurrentValues.LightsValues.BlinkerLeftActive;
                    Truck_Daten.BLINKER_RECHTS = (bool)data.TruckValues.CurrentValues.LightsValues.BlinkerRightActive;
                    Truck_Daten.TEMPOMAT_KMH =(int)data.TruckValues.CurrentValues.DashboardValues.CruiseControlSpeed.Kph;
                    Truck_Daten.TEMPOMAT_MPH = (int)data.TruckValues.CurrentValues.DashboardValues.CruiseControlSpeed.Mph;
                    Truck_Daten.VorwaertsGaenge = (int)data.TruckValues.ConstantsValues.MotorValues.ForwardGearCount;
                    Truck_Daten.RUECKWAERTS_GAENGE = (int)data.TruckValues.ConstantsValues.MotorValues.ReverseGearCount;
                    Truck_Daten.GANG = (int)data.TruckValues.CurrentValues.MotorValues.GearValues.Selected;
                    Truck_Daten.HERSTELLER = data.TruckValues.ConstantsValues.Brand;
                    Truck_Daten.HERSTELLER_ID = data.TruckValues.ConstantsValues.BrandId;
                    Truck_Daten.MODELL = data.TruckValues.ConstantsValues.Name;
                    Truck_Daten.FUEL_MAX = (int)data.TruckValues.ConstantsValues.CapacityValues.Fuel;
                    Truck_Daten.FUEL_BESTAND = (int)data.TruckValues.CurrentValues.DashboardValues.FuelValue.Amount;
                    Truck_Daten.FUEL_REST = (int)data.TruckValues.CurrentValues.DashboardValues.FuelValue.Amount;
                    Truck_Daten.FUEL_VERBRAUCH = data.TruckValues.CurrentValues.DashboardValues.FuelValue.AverageConsumption;
                    Truck_Daten.ADBLUE_MAX = Convert.ToInt32(data.TruckValues.ConstantsValues.CapacityValues.AdBlue).ToString();
                    Truck_Daten.ADBLUE_BESTAND = Convert.ToInt32(data.TruckValues.CurrentValues.DashboardValues.AdBlue).ToString();
                    Truck_Daten.RPM = (int)data.TruckValues.CurrentValues.DashboardValues.RPM;
                    Truck_Daten.RPM_MAX = (int)data.TruckValues.ConstantsValues.MotorValues.EngineRpmMax;
                    // STATUS ANZEIGEN
                    Truck_Daten.ELEKTRIK_STATUS = data.TruckValues.CurrentValues.ElectricEnabled;
                    Truck_Daten.ABBLENDLICHT = (bool)data.TruckValues.CurrentValues.LightsValues.Parking;
                    Truck_Daten.NORMMALLICHT = (bool)data.TruckValues.CurrentValues.LightsValues.BeamLow;
                    Truck_Daten.PARKING_BRAKE = (bool)data.TruckValues.CurrentValues.MotorValues.BrakeValues.ParkingBrake;
                    Truck_Daten.BRAKE_VISIBILITY = (bool)data.TruckValues.CurrentValues.MotorValues.BrakeValues.MotorBrake;

                    // Schadensanzeige
                    Truck_Daten.TRUCK_MOTOR_SCHADEN = Convert.ToInt32(data.TruckValues.CurrentValues.DamageValues.Engine * 100);
                    Truck_Daten.TRUCK_GETRIEBE_SCHADEN = Convert.ToInt32(data.TruckValues.CurrentValues.DamageValues.Transmission * 100);
                    Truck_Daten.TRUCK_FAHRWERK_SCHADEN = Convert.ToInt32(data.TruckValues.CurrentValues.DamageValues.Chassis * 100);
                    Truck_Daten.TRUCK_CHASSIS_SCHADEN = Convert.ToInt32(data.TruckValues.CurrentValues.DamageValues.Cabin * 100);
                    Truck_Daten.TRUCK_RAEDER_SCHADEN = Convert.ToInt32(data.TruckValues.CurrentValues.DamageValues.WheelsAvg * 100);
                    Truck_Daten.TRAILER_FRACHT_SCHADEN = (double)data.JobValues.CargoValues.CargoDamage*100;

                    Truck_Daten.TRAILER_FAHRWERK_SCHADEN = (double)data.TrailerValues[0].DamageValues.Wheels;
                    Truck_Daten.TRAILER_CHASSIS_SCHADEN = (double)data.TrailerValues[0].DamageValues.Chassis;
                    // JOB DATEN
                    Truck_Daten.FRACHT_GELADEN = (bool)data.TrailerValues[0].Attached;
                    Truck_Daten.IS_JOB_DATA_VISIBLE = (bool)data.TrailerValues[0].Attached;
                    Truck_Daten.REST_STRECKE = (int)data.NavigationValues.NavigationDistance / 1000;
                    Truck_Daten.SPEZIAL_JOB = (bool)data.JobValues.SpecialJob;
                    Truck_Daten.MARKET = data.JobValues.Market.ToString();
                    Truck_Daten.START_ORT = (string)data.JobValues.CitySource;
                    Truck_Daten.ZIEL_ORT = (string)data.JobValues.CityDestination;
                    Truck_Daten.START_FIRMA = (string)data.JobValues.CompanySource;
                    Truck_Daten.ZIEL_FIRMA = (string)data.JobValues.CompanyDestination;
                    Truck_Daten.JOB_EINKOMMEN = (double)data.JobValues.Income;
                    Truck_Daten.GEPLANTE_DISTANZ = (int)data.JobValues.PlannedDistanceKm; 
                    Truck_Daten.FRACHT_GEWICHT = (double)data.JobValues.CargoValues.Mass;
                    Truck_Daten.FRACHT_GEWICHT_TONNEN = (int)data.JobValues.CargoValues.Mass / 1000;
                    Truck_Daten.FRACHT_NAME = (string)data.JobValues.CargoValues.Name;
                    // TODO Es fehlen noch :CARGO_ID : UNIT_COUNT : UNIT_MASS und die ganzen ID von Job City Source/Destination etc. Falls benötigt, kurz sagen

                    // NAVIGATION
                    Truck_Daten.NAVIGATION_DISTANZ = (double)data.NavigationValues.NavigationDistance / 1000;
                    Truck_Daten.NAVIGATION_SPEED_LIMIT_KMH = (int)data.NavigationValues.SpeedLimit.Kph;
                    Truck_Daten.NAVIGATION_SPEED_LIMIT_MPH = (int)data.NavigationValues.SpeedLimit.Mph;
                    Truck_Daten.NAVIGATION_ZEIT = (double)data.NavigationValues.NavigationTime;

                    // MAUTSTATIONEN
                     Truck_Daten.MAUT_BEZAHLT = (int)data.GamePlay.TollgateEvent.PayAmount;

                    // TANKEN
                    Truck_Daten.TANKEN_BEZAHLT = (int)data.GamePlay.RefuelEvent.Amount;

                    // STRAFEN
                      Truck_Daten.TANKEN_BEZAHLT = (int)data.GamePlay.FinedEvent.Amount;

                    // FÄHREN
                    Truck_Daten.FAEHRE_BEZAHLT = (int)data.GamePlay.FerryEvent.PayAmount;
                    Truck_Daten.FAEHRE_ABFAHRT_VON = (string)data.GamePlay.FerryEvent.SourceName;
                    Truck_Daten.FAEHRE_ANKUNFT_IN = (string)data.GamePlay.FerryEvent.TargetName;

                    // JOB ABGABE
                    Truck_Daten.ABGABE_JOB_BEENDET = data.GamePlay.JobDelivered.Finished.ToString();
                    Truck_Daten.ABGABE_AUTOPARKING = (bool)data.GamePlay.JobDelivered.AutoParked;
                    Truck_Daten.ABGABE_FRACHTSCHADEN = (double)data.GamePlay.JobDelivered.CargoDamage;
                    Truck_Daten.ABGABE_ABGABEZEIT = data.GamePlay.JobDelivered.DeliveryTime.ToString();
                    Truck_Daten.ABGABE_DISTANZ_KM = (int)data.GamePlay.JobDelivered.DistanceKm;
                    Truck_Daten.ABGABE_XP = (int)data.GamePlay.JobDelivered.EarnedXp;
                    Truck_Daten.ABGABE_EINNAHMEN = (double)data.GamePlay.JobDelivered.Revenue;

                    // Definition ETS / ATS
                    Truck_Daten.ANZEIGE_LITER_GALLONEN = (data.Game.ToString() == "Ets2") ? "Liter" : "Gall.";

                    // TEXTANZEIGE IM MAINVIEW-STREETVIEW
                    // TODO TRANSLATION DE EN
                    Truck_Daten.TXT_FAHRT = "Du fährst " + Truck_Daten.FRACHT_GEWICHT_TONNEN + Truck_Daten.ANZEIGE_TO_LBS + data.JobValues.CargoValues.Name + " von" + Environment.NewLine;
                    Truck_Daten.TXT_FAHRT += "Firma " + data.JobValues.CompanySource + " in " + data.JobValues.CitySource + Environment.NewLine;
                    Truck_Daten.TXT_FAHRT += "Zur Firma " + data.JobValues.CompanyDestination + " in " +data.JobValues.CityDestination + " fahren !" +Environment.NewLine;
                    Truck_Daten.TXT_FAHRT += Environment.NewLine;
                    Truck_Daten.TXT_FAHRT += "Du musst noch " + Truck_Daten.REST_STRECKE + Truck_Daten.ANZEIGE_KM_MILES + " von " + Truck_Daten.REST_STRECKE + Truck_Daten.ANZEIGE_KM_MILES + " fahren !";
                   
                   // Image finalImage = new Image();



                }


            }
            catch
            { }

        }


        public void lade_Translations()
        {
            translation = new Translation(utils.Reg_Lesen("Config", "Sprache", false));
            TAB_FAHRT.Header = translation.TAB_Fahrt_Text;
            TAB_FREUNDE.Header = translation.TAB_FREUNDE_TEXT;
            TAB_VERKEHR.Header = translation.TAB_VERKEHR_TEXT;
            TAB_EINSTELLUNGEN.Header = translation.TAB_EINSTELLUNGEN_TEXT;
            LBL_FAHRT_SCHADEN_TITEL.Content = translation.SCHADENSANZEIGE_TITEL;
            LBL_Truck_Name.Content = translation.TRUCK_NAME;
            LBL_Truck_Model.Content = translation.TRUCK_MODELL;
            TAB_FAHRT_LBL_MOTOR.Content = translation.TAB_FAHRT_LBL_MOTOR;
            TAB_FAHRT_LBL_GETRIEBE.Content = translation.TAB_FAHRT_LBL_GETRIEBE;
            TAB_FAHRT_LBL_RAEDER.Content = translation.TAB_FAHRT_LBL_RAEDER;
            TAB_FAHRT_LBL_FAHRWERK.Content = translation.TAB_FAHRT_LBL_FAHRWERK;
            TAB_FAHRT_LBL_FAHRERHAUS.Content = translation.TAB_FAHRT_LBL_FAHRERHAUS;
            TAB_FAHRT_LBL_FRACHTSCHADEN.Content = translation.TAB_FAHRT_LBL_FRACHTSCHADEN;
            BUTTON_ATS_STARTEN.Text = translation.BUTTON_ATS_STARTEN;
            BUTTON_ETS_STARTEN.Text = translation.BUTTON_ETS_STARTEN;
            BUTTON_TMP_STARTEN.Text = translation.BUTTON_TMP_STARTEN;



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
            FileHandler.StarteAnwendung("https://www.facebook.com/groups/vtcmanager");
        }

        private void LaunchDiscord(object sender, RoutedEventArgs e)
        {
            FileHandler.StarteAnwendung("https://discord.gg/tye7APA");
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
            Translation trans = new Translation(utils.Reg_Lesen("Config", "Sprache", false));
            if (string.IsNullOrEmpty(utils.Reg_Lesen("Config", "ATS_Pfad", false)))
                Show_Message(trans.ETS2_PATH_NOT_FOUND);

            FileHandler.StarteAnwendung(utils.Reg_Lesen("Config", "ATS_Pfad", false));
        }

        private void Launch_ETS(object sender, RoutedEventArgs e)
        {
            Translation trans = new Translation(utils.Reg_Lesen("Config", "Sprache", false));
            if (string.IsNullOrEmpty(utils.Reg_Lesen("Config", "ETS2_Pfad", false)))
                Show_Message(trans.ATS_PATH_NOT_FOUND);

            FileHandler.StarteAnwendung(utils.Reg_Lesen("Config", "ETS2_Pfad", false));
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

        private void Minimize_Window(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
