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
using MahApps.Metro.Controls;

using VTCManager.Klassen;

namespace VTCManager
{
    /// <summary>
    /// Interaktionslogik für LogIn.xaml
    /// </summary>
    public partial class RegisterClient
    {
        public RegisterClient()
        {
            if (!String.IsNullOrEmpty(RegistryHandler.read("Config", "AccessToken")))
            {
                Config.AccessToken = RegistryHandler.read("Config", "AccessToken");
                //Check the key
                JObject response = API.HTTPSRequestGet(API.get_user);
                Console.WriteLine(response);
                //when no error key was found, the login was successful
                if (response["error"] != null)
                {
                    if ((bool)response["error"] == true && (int)response["error_code"] == 401)
                    {
                        InitializeComponent();
                        LoginButton.Click += LoginButton_Click;
                        MessageBox.Show("Der Key existiert nicht. Anmeldung fehlgeschlange.", "FEHLER", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else
                    {
                        InitializeComponent();
                        LoginButton.Click += LoginButton_Click;
                        MessageBox.Show("Ein unbekannter Fehler ist beim Anmelden aufgetreten", "FEHLER", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
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
            Config.AccessToken = key_input;
            //Check the key
            JObject response = API.HTTPSRequestGet(API.get_user);
            //when no error key was found, the login was successful
            if(response["error"] != null)
            {
                if ((bool)response["error"] == true && (int)response["error_code"] == 401)
                {
                    MessageBox.Show("Der Key existiert nicht. Anmeldung fehlgeschlange.", "FEHLER", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            //safe the key
            RegistryHandler.write("AccessToken", key_input, "Config");
            //open MainInterface
            MainWindow mainwin = new MainWindow();
            mainwin.Show();
            this.Close();
        }
    }
}
