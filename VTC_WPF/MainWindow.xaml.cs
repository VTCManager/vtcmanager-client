using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using SCSSdkClient;
using SCSSdkClient.Object;

namespace VTC_WPF
{
    public partial class MainWindow : Window
    {
        public SCSSdkTelemetry Telemetry;
        private object rect1;
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
            this.Telemetry.Data += Telemetry_Data;
            this.Telemetry.JobStarted += this.TelemetryOnJobStarted;
            this.Telemetry.JobCancelled += TelemetryJobCancelled;
            this.Telemetry.JobDelivered += TelemetryJobDelivered;
            this.Telemetry.Fined += TelemetryFined;
            this.Telemetry.Tollgate += TelemetryTollgate;
            this.Telemetry.Ferry += TelemetryFerry;
            this.Telemetry.Train += TelemetryTrain;
            this.Telemetry.RefuelStart += TelemetryRefuelStart;
            this.Telemetry.RefuelEnd += TelemetryRefuelEnd;
            this.Telemetry.RefuelPayed += TelemetryRefuelPayed;


        }

        private void Telemetry_Data(SCSTelemetry data, bool updated)
        {
            try
            {

                if (this.InvokeRequired)
                {
                    // Invoke((Delegate)new TelemetryData(this.Telemetry_Data), (object)data, (object)updated);
                }
                else
                {
                    UpdateLabelContent(this.RPM_lbl, Convert.ToInt32(data.TruckValues.CurrentValues.DashboardValues.RPM).ToString() + " R/PM");
                    UpdateLabelContent(this.speed_label_tacho, Convert.ToInt32(data.TruckValues.CurrentValues.DashboardValues.Speed.Kph).ToString());
                    UpdateLabelContent(this.Speed_Limit_Label, Convert.ToInt32(data.NavigationValues.SpeedLimit.Kph).ToString());
                    UpdateLabelContent(this.Speed_Limiter_Setting, Convert.ToInt32(data.TruckValues.CurrentValues.DashboardValues.CruiseControlSpeed.Kph).ToString());
                    
                    if(data.TruckValues.CurrentValues.MotorValues.GearValues.Selected == -1)
                        UpdateLabelContent(this.Gang_Label, "R");

                    UpdateLabelContent(this.Gang_Label, Convert.ToInt32(data.TruckValues.CurrentValues.MotorValues.GearValues.Selected).ToString());

                }

            } catch
            {

            }
        }


        private void TelemetryRefuelStart(object sender, EventArgs e)
        {
         
        }

        private void TelemetryRefuelPayed(object sender, EventArgs e)
        {
          
        }

        private void TelemetryRefuelEnd(object sender, EventArgs e)
        {
        
        }

        private void TelemetryTrain(object sender, EventArgs e)
        {
    
        }

        private void TelemetryFerry(object sender, EventArgs e)
        {
         
        }

        private void TelemetryFined(object sender, EventArgs e)
        {
          
        }

        private void TelemetryTollgate(object sender, EventArgs e)
        {

        }

        private void TelemetryJobDelivered(object sender, EventArgs e)
        {
         
        }

        private void TelemetryJobCancelled(object sender, EventArgs e)
        {
          
        }

        private void TelemetryOnJobStarted(object sender, EventArgs e)
        {
       
        }

        public void UpdateLabelContent(Label label, string newContent)
        {
            Dispatcher.Invoke(new UpdateProgressDelegate(label.SetValue), DispatcherPriority.Background, ContentProperty, newContent);
        }

        private void Rotate(float grad)
        {
            X1 = 0;
            Y1 = 0;
            var rad = Degrees * Math.PI / grad;
            const int radius = 180;
            var sin = Math.Sin(rad);
            var cos = Math.Cos(rad);
            var tan = Math.Tan(rad);

            Y2 = sin * radius;
            X2 = cos * radius;

        }

    }
}
