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

        public int Degrees { get; private set; }
        public double Y2 { get; private set; }
        public double X2 { get; private set; }
        public int X1 { get; private set; }
        public int Y1 { get; private set; }

        private delegate void UpdateProgressDelegate(DependencyProperty dp, object value);


        public MainWindow()
        {
            InitializeComponent();
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


        }

        private void Telemetry_Data(SCSTelemetry data, bool updated)
        {
            try
            {

                if (InvokeRequired)
                {
                    // Invoke((Delegate)new TelemetryData(this.Telemetry_Data), (object)data, (object)updated);
                }
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
            {

            }
        }

        public void UpdateLabelContent(Label label, string newContent)
        {
            Dispatcher.Invoke(new UpdateProgressDelegate(label.SetValue), DispatcherPriority.Background, ContentProperty, newContent);
        }

        private void Rotate(float grad)
        {
            X1 = 0;
            Y1 = 0;
            double rad = Degrees * Math.PI / grad;
            const int radius = 180;
            double sin = Math.Sin(rad);
            double cos = Math.Cos(rad);
            double tan = Math.Tan(rad);

            Y2 = sin * radius;
            X2 = cos * radius;

        }

    }
}
