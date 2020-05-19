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
            TelemetryInstaller.install2();
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
                        UpdateLabelContent(RPM_lbl, Convert.ToInt32(data.TruckValues.CurrentValues.DashboardValues.RPM).ToString() + " R/PM");
                        UpdateLabelContent(speed_label_tacho, Convert.ToInt32(data.TruckValues.CurrentValues.DashboardValues.Speed.Kph).ToString());
                        UpdateLabelContent(Speed_Limit_Label, Convert.ToInt32(data.NavigationValues.SpeedLimit.Kph).ToString());
                        UpdateLabelContent(Speed_Limiter_Setting, Convert.ToInt32(data.TruckValues.CurrentValues.DashboardValues.CruiseControlSpeed.Kph).ToString());

                        if (data.TruckValues.CurrentValues.MotorValues.GearValues.Selected == -1)
                        {
                            UpdateLabelContent(Gang_Label, "R");
                        }
                        UpdateLabelContent(Gang_Label, Convert.ToInt32(data.TruckValues.CurrentValues.MotorValues.GearValues.Selected).ToString());
                }
            }
            catch
            { }
            
        }

        public void UpdateLabelContent(Label label, string newContent)
        {
                Dispatcher.Invoke(new UpdateProgressDelegate(label.SetValue), DispatcherPriority.Background, ContentProperty, newContent);
        }


    }
}
