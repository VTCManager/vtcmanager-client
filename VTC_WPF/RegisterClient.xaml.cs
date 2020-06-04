using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
using VTC_WPF.Klassen;

namespace VTC_WPF
{
    /// <summary>
    /// Interaktionslogik für LogIn.xaml
    /// </summary>
    public partial class RegisterClient : Window
    {
        public RegisterClient()
        {
            if (!String.IsNullOrEmpty(RegistryHandler.read("Config", "ClientKey")))
            {
                //open MainInterface
                MainWindow mainwin = new MainWindow();
                mainwin.Show();
                this.Close();
            }
            else
            {
                InitializeComponent();
                LoginButton.Click += LoginButton_Click;
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            String key_input = InputClientKey.Text;
            if (String.IsNullOrWhiteSpace(key_input))
                return;
            //get mac adress 
            var macAddr =
            (
                from nic in NetworkInterface.GetAllNetworkInterfaces()
                where nic.OperationalStatus == OperationalStatus.Up
                select nic.GetPhysicalAddress().ToString()
            ).FirstOrDefault();
            //Check the key
            Dictionary<string, string> client_ident = new Dictionary<string, string>();
            client_ident.Add("client_ident", macAddr.ToString());
            JObject response = API.HTTPSRequestPost(API.register + key_input, client_ident);
            var parsed_response = response.SelectToken("data");
            if((bool)parsed_response["success"] == false)
            {
                return;
            }
            //safe the key
            RegistryHandler.write("ClientKey", key_input, "Config");
            //open MainInterface
            MainWindow mainwin = new MainWindow();
            mainwin.Show();
            this.Close();
        }
    }
}
