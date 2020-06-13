using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VTCManager
{
    /// <summary>
    /// Interaktionslogik für ETS2PathDialog.xaml
    /// </summary>
    public partial class ETS2PathDialog : Window
    {
        public ETS2PathDialog()
        {
            InitializeComponent();
        }
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



            dlg.Title = "Bitte gib den Pfad zum Euro Truck Simulator 2 Ordner an";
            dlg.FileName = "eurotrucks2.exe";
            dlg.Filter = "eurotrucks2.exe|eurotrucks2.exe";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                Console.WriteLine(filename);
            }
        }
    }
}
