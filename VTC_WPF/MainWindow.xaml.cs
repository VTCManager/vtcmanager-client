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
using System.Windows.Navigation;
using System.Runtime.InteropServices;
using System.IO;
using VTCManager.Pages;

namespace VTCManager
{
    public partial class MainWindow : MetroWindow
    {
        public SCSSdkTelemetry Telemetry;
        private readonly bool InvokeRequired;
        private delegate void UpdateProgressDelegate(DependencyProperty dp, object value);
        public Truck_Daten Truck_Daten;
        Klassen.Utilities utils = new Klassen.Utilities();
        public JobHandler jobHandler;
        private OpenFileDialog tmp_Trucker;
        public int Gesch2;
        int minutes;
        int Tankinhalt;
        private object polyline1;
        private TelemetryHandler telemetryhandler;
        private Translation translation;
        DispatcherTimer updateHersteller_Image = new DispatcherTimer();
        public string int_game_version;
        public int NAVIGATION_SPEED_LIMIT { get; private set; }
        public string BILD_ANZEIGE2 { get; private set; }

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

            MainFrame.Content = new Fahrt_Page();

            Lade_Voreinstellungen();
            lade_Translations();
            //must be after lade_Translations
            telemetryhandler = new TelemetryHandler(this, translation);
            if (string.IsNullOrEmpty(RegistryHandler.read("Config", "Sprache")))
                RegistryHandler.write("Sprache", "", "Config");



            this.DataContext = Truck_Daten;
        }
        public async void Show_Message(string text)
        {
            await this.ShowMessageAsync(text, "Das Team von VTC-Manager.");
        }

        private void Lade_Voreinstellungen()
        {
            Sprachauswahl.SelectedValue = utils.Reg_Lesen("Config", "Sprache", false);

            var accent = RegistryHandler.read("Config", "Design");
            if (string.IsNullOrEmpty(accent)) accent = "Dark.Blue";
            ThemeManager.Current.ChangeTheme(this, "Dark.Blue");
        }



        private void Telemetry_Data_Handler(SCSTelemetry data, bool updated)
        {
            try
            {
                if (!InvokeRequired)
                {
                    // ALLGEMEINES
                    Truck_Daten.TELEMETRY_VERSION = "Telemetry: " + data.TelemetryVersion.Major.ToString() + "." + data.TelemetryVersion.Minor.ToString();
                    Truck_Daten.SPIEL_VERSION = "Game: " + data.GameVersion.Major.ToString() + "." + data.GameVersion.Minor.ToString();
                    Truck_Daten.DLL_VERSION = "DLL: " + data.DllVersion.ToString();
                    Truck_Daten.SPIEL = data.Game.ToString();

                    Truck_Daten.IS_GAME_RUNNING = data.Paused;
                    Truck_Daten.SDK_ACTIVE = data.SdkActive;
                    Truck_Daten.SPIEL = data.Game.ToString();

                    Truck_Daten.ANZEIGE_KM_MILES = (Truck_Daten.SPIEL == "Ets2") ? " km " : " mi";
                    Truck_Daten.ANZEIGE_TO_LBS = (Truck_Daten.SPIEL == "Ets2") ? " t " : " lb ";
                    Truck_Daten.SPEED_KMH = (int)data.TruckValues.CurrentValues.DashboardValues.Speed.Kph;
                    Truck_Daten.SPEED_TACHO_KMH = (int)data.TruckValues.CurrentValues.DashboardValues.Speed.Kph-85;
                    Truck_Daten.SPEED_MPH = (int)data.TruckValues.CurrentValues.DashboardValues.Speed.Kph;
                    Truck_Daten.SPEED_TACHO_MPH = (int)data.TruckValues.CurrentValues.DashboardValues.Speed.Kph - 85;
                    Truck_Daten.BLINKER_LINKS = (bool)data.TruckValues.CurrentValues.LightsValues.BlinkerLeftOn;
                    Truck_Daten.BLINKER_RECHTS = (bool)data.TruckValues.CurrentValues.LightsValues.BlinkerRightOn;
                    Truck_Daten.TEMPOMAT_KMH =(int)data.TruckValues.CurrentValues.DashboardValues.CruiseControlSpeed.Kph;
                    Truck_Daten.TEMPOMAT_MPH = (int)data.TruckValues.CurrentValues.DashboardValues.CruiseControlSpeed.Mph;
                    Truck_Daten.VorwaertsGaenge = (int)data.TruckValues.ConstantsValues.MotorValues.ForwardGearCount;
                    Truck_Daten.RUECKWAERTS_GAENGE = (int)data.TruckValues.ConstantsValues.MotorValues.ReverseGearCount;
                    Truck_Daten.GANG = (int)data.TruckValues.CurrentValues.MotorValues.GearValues.Selected;
                    Truck_Daten.HERSTELLER = data.TruckValues.ConstantsValues.Brand;
                    Truck_Daten.HERSTELLER_ID = data.TruckValues.ConstantsValues.BrandId;
                    Truck_Daten.MODELL = translation.TRUCK_MODELL + data.TruckValues.ConstantsValues.Name;
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

                    Truck_Daten.ANZEIGE_SPEED_LIMIT = Truck_Daten.SPIEL == "Ets2" ? Truck_Daten.NAVIGATION_SPEED_LIMIT_KMH.ToString() : Truck_Daten.NAVIGATION_SPEED_LIMIT_MPH.ToString();

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
                    Truck_Daten.TXT_FAHRT = "Du fährst " + Truck_Daten.FRACHT_GEWICHT_TONNEN + Truck_Daten.ANZEIGE_TO_LBS + data.JobValues.CargoValues.Name + " von ";
                    Truck_Daten.TXT_FAHRT += data.JobValues.CitySource + " nach ";
                    Truck_Daten.TXT_FAHRT += data.JobValues.CityDestination;
                    Truck_Daten.TXT_FAHRT += Environment.NewLine;
                    Truck_Daten.TXT_FAHRT += "Du musst noch " +  Truck_Daten.REST_STRECKE + Truck_Daten.ANZEIGE_KM_MILES + " fahren !";

                }
            }
            catch
            { }

        }

        public void lade_Translations()
        {
            translation = new Translation(utils.Reg_Lesen("Config", "Sprache", false));
        }

        private void LaunchDiscord(object sender, RoutedEventArgs e)
        {
            FileHandler.StarteAnwendung("https://discord.gg/tye7APA");
        }

        private void LaunchWebsite(object sender, RoutedEventArgs e)
        {
       
            FileHandler.StarteAnwendung("https://vtc.northwestvideo.de/");
        }

        private void Sprachauswahl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string sprache = (string)Sprachauswahl.SelectedValue;

            if (utils.Reg_Lesen("Config", "Sprache", false) != (string)Sprachauswahl.SelectedValue)
            {
                Show_Message(translation.CHANGE_LANGUAGE);
            }

            utils.Reg_Schreiben("Sprache", sprache, "Config");
            Translation Translation = new Translation(utils.Reg_Lesen("Config", "Design", false));
        }

        private void Minimize_Window(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void LaunchFaceBookSite(object sender, RoutedEventArgs e)
        {
            FileHandler.StarteAnwendung("https://www.facebook.com/groups/vtcmanager");
        }

        private void LaunchGitHubSite(object sender, RoutedEventArgs e)
        {
            FileHandler.StarteAnwendung("https://github.com/VTCManager");
        }


        internal sealed class win32
        {
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            internal static extern int SystemParametersInfo(
                int uAction,
                int uParam, String lpvParam,
                int fuWinIni);
        }

        private void set_Desktop_Background(string sFile_Full_Path)
        {
            const int SET_DESKTOP_BACKGROUND = 20;
            const int UPDATE_INI_FILE = 1;
            const int SEND_WINDOWS_INI_CHANGE = 2;
            win32.SystemParametersInfo(SET_DESKTOP_BACKGROUND, 0, sFile_Full_Path, UPDATE_INI_FILE | SEND_WINDOWS_INI_CHANGE);
        }

        private void wp_1_set_Click(object sender, RoutedEventArgs e)
        {
            string sFile_Full_Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\VTCManager\Wallpaper1.png";
            set_Desktop_Background(sFile_Full_Path);
        }

        private void Farbschema_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RegistryHandler.write("Design", (string)(Farbschema.SelectedValue), "Config");
            try
            {
                ThemeManager.Current.ChangeTheme(this, (string)(Farbschema.SelectedValue));
            }
            catch (Exception ex)
            {
                Logging.WriteClientLog("Designer: Konnte das Design " + (string)(Farbschema.SelectedValue) + " nicht laden: " + Environment.NewLine + ex.Message + ex.StackTrace);
            }
        }


    }
}
