using Microsoft.Win32;
using SCSSdkClient;
using SCSSdkClient.Object;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Threading;
using VTC_WPF.Klassen;

namespace VTC_WPF
{
    public partial class MainWindow : Window
    {
        public SCSSdkTelemetry Telemetry;
        private readonly bool InvokeRequired;
        private delegate void UpdateProgressDelegate(DependencyProperty dp, object value);
        public DiscordHandler Discord;
        Utilities utils = new Utilities();
        public JobHandler jobHandler;
        private OpenFileDialog tmp_Trucker;
        public int Gesch2;
        int minutes;

        public MainWindow()
        {
            Logging.Make_Log_File(); // Muss als erstes stehen, damit vor allem anderen die Logs geleert werden !
            TelemetryInstaller.check();
            Telemetry = new SCSSdkTelemetry();
            Telemetry.Data += Telemetry_Data;
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

            InitializeComponent();


            TMP_Pfad_Textbox.Text = utils.Reg_Lesen("Config", "TMP_PFAD", true);
            TMP_Pfad_Textbox.IsEnabled = (string.IsNullOrEmpty(utils.Reg_Lesen("Config", "TMP_PFAD", true))) ? true : false;



        }


        private void Telemetry_Data(SCSTelemetry data, bool updated)
        {
            try
            {

                if (!InvokeRequired)
                {

                    UpdateLabelContent(Truck_Manufactur_Label, data.TruckValues.ConstantsValues.Brand.ToString() + ", Modell: " + data.TruckValues.ConstantsValues.Name.ToString());
                    UpdateLabelContent(Testlabel, Convert.ToInt32(data.TruckValues.CurrentValues.DashboardValues.Speed.Kph).ToString() + " KM/H");
                    UpdateLabelContent(Tour_Startort, "Du fährst " + data.JobValues.PlannedDistanceKm.ToString() + " km von " + data.JobValues.CitySource.ToString() + " nach " + data.JobValues.CityDestination.ToString());
                    UpdateLabelContent(Label_Streckeninfos, "Deine Wegstrecke beträgt jetzt noch " + (int)(data.NavigationValues.NavigationDistance / 1000) + " KM");
                    minutes = ((int)data.JobValues.RemainingDeliveryTime.Value >= 1) ? (int)data.JobValues.RemainingDeliveryTime.Value : 0;

                    UpdateLabelContent(Label_Streckeninfos_2, "Restzeit: " + String.Format("{0} Std. {1} Min.", minutes / 60, minutes % 60));


                    // Tankanzeige

                    UpdateLabelContent(Label_Fuel_Current, Convert.ToInt32(data.TruckValues.CurrentValues.DashboardValues.FuelValue.Amount).ToString() + " l");
                    UpdateLabelContent(Label_Fuel_Maximum, Convert.ToDouble(data.TruckValues.ConstantsValues.CapacityValues.Fuel).ToString() + " l");

                   
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
