using System.Windows.Controls;

namespace VTCManager.Pages
{
   
    public partial class Fahrt_Page : Page
    {
        Truck_Daten td = new Truck_Daten();

        public Fahrt_Page()
        {
            InitializeComponent();

            string SPEED = td.SPEED_KMH.ToString();

        }



    }
}
