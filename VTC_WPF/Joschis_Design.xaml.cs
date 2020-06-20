using SCSSdkClient;
using SCSSdkClient.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using VTCManager.Klassen;
using Label = System.Windows.Forms.Label;

namespace VTCManager
{
    /// <summary>
    /// Interaktionslogik für Joschis_Design.xaml
    /// </summary>
    public partial class Joschis_Design : Window
    {
        public SCSSdkTelemetry Telemetry;
        private readonly bool InvokeRequired;
        private delegate void UpdateProgressDelegate(DependencyProperty dp, object value);
        Klassen.Utilities utils = new Klassen.Utilities();
        public JobHandler jobHandler;
        private OpenFileDialog tmp_Trucker;
        public int Gesch2;
        int minutes;
        int Tankinhalt;
        private object polyline1;
        public Joschis_Design()
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

            InitializeComponent();
        }
        private void Telemetry_Data(SCSTelemetry data, bool updated)
        {
            try
            {

                if (!InvokeRequired)
                {
                    DateTime remaindelivtime = data.JobValues.RemainingDeliveryTime.Date;
                    DateTime gametime = data.CommonValues.GameTime.Date;
                    double percentage_tour = Convert.ToDouble(data.NavigationValues.NavigationDistance)/ Convert.ToDouble(data.JobValues.PlannedDistanceKm);
                    if(!Double.IsNaN(percentage_tour))
                        Progessbar.Dispatcher.Invoke(() => Progessbar.Value = percentage_tour, DispatcherPriority.Background);
                    if(data.NavigationValues.NavigationDistance > 2000)
                    {
                        Progessbar.Dispatcher.Invoke(() => DistanceLeft.Content = data.NavigationValues.NavigationDistance/1000, DispatcherPriority.Background);
                        Progessbar.Dispatcher.Invoke(() => DistanceLeftUnit.Content = "km", DispatcherPriority.Background);
                    }
                    else
                    {
                        Progessbar.Dispatcher.Invoke(() => DistanceLeft.Content = data.NavigationValues.NavigationDistance, DispatcherPriority.Background);
                        Progessbar.Dispatcher.Invoke(() => DistanceLeftUnit.Content = "m", DispatcherPriority.Background);
                    }
                    Progessbar.Dispatcher.Invoke(() => MaxSpeed.Content = data.NavigationValues.SpeedLimit, DispatcherPriority.Background);
                    Progessbar.Dispatcher.Invoke(() => SpeedLabel.Content = data.TruckValues.CurrentValues.DashboardValues.Speed.Kph, DispatcherPriority.Background);

                }


            }
            catch
            { }

        }
    }
}
