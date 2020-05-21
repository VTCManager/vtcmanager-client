using SCSSdkClient;
using SCSSdkClient.Object;
using System;
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
        public JobHandler jobHandler;
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
        }

        private void Telemetry_Data(SCSTelemetry data, bool updated)
        {
            try
            {

                if (InvokeRequired) { }
                else
                {
                        //UpdateLabelContent(Dashboard_RPM, Convert.ToInt32(data.TruckValues.CurrentValues.DashboardValues.RPM).ToString() + " R/PM");
                        //UpdateLabelContent(Dashboard_Speed, Convert.ToInt32(data.TruckValues.CurrentValues.DashboardValues.Speed.Kph).ToString());
           
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

      
    }
}
