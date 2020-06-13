using Microsoft.Win32;
using SCSSdkClient;
using SCSSdkClient.Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using VTCManager.Klassen;

namespace VTCManager
{
    public partial class MainWindow : Window
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

        public string TEST { get; set; }
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

                    /* TODO UMBAU 13-06-2020

                    UpdateLabelContent(Truck_Manufactur_Label, data.TruckValues.ConstantsValues.Brand.ToString() + ", Modell: " + data.TruckValues.ConstantsValues.Name.ToString());
                    
                    UpdateLabelContent(Tour_Startort, "Du fährst " + data.JobValues.PlannedDistanceKm.ToString() + " km von " + data.JobValues.CitySource.ToString() + " nach " + data.JobValues.CityDestination.ToString());
                    UpdateLabelContent(Label_Streckeninfos, "Deine Wegstrecke beträgt jetzt noch " + (int)(data.NavigationValues.NavigationDistance / 1000) + Environment.NewLine + " KM");
                    minutes = ((int)data.JobValues.RemainingDeliveryTime.Value >= 1) ? (int)data.JobValues.RemainingDeliveryTime.Value : 0;

                    UpdateLabelContent(Label_Streckeninfos_2, "Restzeit: " + String.Format("{0} Std. {1} Min.", minutes / 60, minutes % 60));
                    
                    */
                    UpdateLabelContent(Speedlabel, Convert.ToInt32(TelemetryHandler.Telemetry_Data.TruckValues.CurrentValues.DashboardValues.Speed.Kph).ToString() + " KM/H");
                    UpdateLabelContent(speed_fuer_tacho, Convert.ToInt32(data.TruckValues.CurrentValues.DashboardValues.Speed.Kph-85).ToString());

         


                    Truck_Daten.HERSTELLER = data.TruckValues.ConstantsValues.Brand;
                    Truck_Daten.MODELL = data.TruckValues.ConstantsValues.Name;

                    Truck_Daten.FUEL_MAX = Convert.ToInt32(data.TruckValues.ConstantsValues.CapacityValues.Fuel).ToString();
                    Truck_Daten.FUEL_BESTAND = Convert.ToInt32(data.TruckValues.CurrentValues.DashboardValues.FuelValue.Amount).ToString();
                    Truck_Daten.ADBLUE_MAX = Convert.ToInt32(data.TruckValues.ConstantsValues.CapacityValues.AdBlue).ToString();
                    Truck_Daten.ADBLUE_BESTAND = Convert.ToInt32(data.TruckValues.CurrentValues.DashboardValues.AdBlue).ToString();

                    if (Truck_Daten.HERSTELLER == "Mercedes-Benz")
                       BRAND_IMAGE.Source = new BitmapImage(new Uri(@"/Icons/icons8-mercedes-benz-256.png"));

                    // TODO Alle Daten müssen hier an die Truck_Daten gebunden werden. Danach in der XAML mit dem Content an den VALUE binden (RPM / RAD_SCHADEN / ZIEL_FIRMA etc)
                    Truck_Daten.RPM = data.TruckValues.CurrentValues.DashboardValues.RPM.ToString();
                    Truck_Daten.RAD_SCHADEN = data.TruckValues.CurrentValues.DamageValues.WheelsAvg.ToString();


                }


            }
            catch
            { }

        }

        public void UpdateLabelContent(Label label, string newContent)
        {
            Dispatcher.Invoke(new UpdateProgressDelegate(label.SetValue), DispatcherPriority.Background, ContentProperty, newContent);
           
        }

        private void MenuIcon_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MenuRightStack.Visibility = (MenuRightStack.IsVisible) ? Visibility.Hidden : Visibility.Visible;
        }

        private void btn_Settings_Click(object sender, RoutedEventArgs e)
        {
            MenuRightStack.Visibility = Visibility.Hidden;
  
            MessageBox.Show("Settings", "Settings", MessageBoxButton.OK, MessageBoxImage.Information);
         
        }

        private void btn_Overlay_Click(object sender, RoutedEventArgs e)
        {
            MenuRightStack.Visibility = Visibility.Hidden;
            MessageBox.Show("Overlay", "Overlay", MessageBoxButton.OK, MessageBoxImage.Information);
          
        }

        private void btn_Logout_Click(object sender, RoutedEventArgs e)
        {
            MenuRightStack.Visibility = Visibility.Hidden;
            MessageBox.Show("Logout", "Logout", MessageBoxButton.OK, MessageBoxImage.Information);
            
        }

        private void btn_Ueber_Click(object sender, RoutedEventArgs e)
        {
            MenuRightStack.Visibility = Visibility.Hidden;
            MessageBox.Show("Über", "Über", MessageBoxButton.OK, MessageBoxImage.Information);
        
        }


        private void btn_open_TMP_File_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                TMP_Pfad_Textbox.Text = openFileDialog.FileName.ToString(); utils.Reg_Schreiben("TMP_PFAD", openFileDialog.FileName.ToString(), "Config");
        }

        private void TMP_Starten_Click(object sender, RoutedEventArgs e)
        {
            FileHandler.StarteAnwednung(utils.Reg_Lesen("Config", "TMP_PFAD", true));

        }


    }
}
