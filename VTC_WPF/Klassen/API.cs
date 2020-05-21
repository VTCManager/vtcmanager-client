﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;

namespace VTC_WPF.Klassen
{

    class API
    {
        public string login_path;
        public string api_server;
        public string load_data_path;
        public string canceltourpath;
        public string finishjob_path;
        public string loc_update_path;
        public string get_traffic_path;
        public string job_update_path;
        public string tollgate_path;
        public string new_job_path;
        public string trucky_api_server;
        public string traffic_data;
        public string get_cities_path = "load_cities.php";
        public string get_cities_path_to = "load_cities_to.php";
        public string load_firmen_in_city = "load_firmen_in_city.php";


        public API()
        {
            login_path = "login.php";
            //this.api_server = "https://vtc.northwestvideo.de/api/app/";
            api_server = "https://vtc.northwestvideo.de/api/app_beta/";
            traffic_data = "http://vtc.northwestvideo.de/api/web/traffic/data";
            trucky_api_server = "https://api.truckyapp.com/v2/";
            load_data_path = "load_data.php";
            canceltourpath = "cancel_tour.php";
            job_update_path = "job_update.php";
            finishjob_path = "job_finish.php";
            new_job_path = "start_tour.php";
            tollgate_path = "tollgate.php";
            loc_update_path = "loc_update.php";
            get_traffic_path = "traffic/top";
        }

        public string HTTPSRequestGet(string url, Dictionary<string, string> getParameters = null)
        {
            string str = "";
            if (getParameters != null)
            {
                foreach (string str3 in getParameters.Keys)
                {
                    string[] textArray1 = new string[] { str, HttpUtility.UrlEncode(str3), "=", HttpUtility.UrlEncode(getParameters[str3]), "&" };
                    str = string.Concat(textArray1);
                }
            }
            HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(url + ((getParameters != null) ? ("?" + str) : ""));
            request1.UserAgent = "VTCManager 1.0.0";
            request1.AutomaticDecompression = DecompressionMethods.GZip;
            WebResponse response = request1.GetResponse();
            string str2 = null;
            using (Stream stream = response.GetResponseStream())
            {
                str2 = new StreamReader(stream).ReadToEnd();
            }
            Console.WriteLine(str2);
            response.Close();
            return str2;
        }
        public string HTTPSRequestPost(string url, Dictionary<string, string> postParameters, bool outputError = true)
        {
            string s = "";
            foreach (string str2 in postParameters.Keys)
            {
                string[] textArray1 = new string[] { s, HttpUtility.UrlEncode(str2), "=", HttpUtility.UrlEncode(postParameters[str2]), "&" };
                s = string.Concat(textArray1);
            }
            try
            {
                HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(url);
                request1.Method = "POST";
                byte[] bytes = Encoding.ASCII.GetBytes(s);
                request1.ContentType = "application/x-www-form-urlencoded";
                request1.UserAgent = "VTCManager 1.0.0";
                request1.ContentLength = bytes.Length;
                Stream requestStream = request1.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                HttpWebResponse response = (HttpWebResponse)request1.GetResponse();
                StreamReader reader1 = new StreamReader(response.GetResponseStream(), Encoding.Default);
                string str3 = reader1.ReadToEnd();
                reader1.Close();
                response.GetResponseStream().Close();
                response.Close();
                Console.WriteLine(str3);
                Console.WriteLine(s);
                return str3;


            }
            catch (WebException exception)
            {
                if (outputError)
                {
                    try
                    {
                        MessageBox.Show("The program will now close!\r\n An error has occurred: \r\n" + ((HttpWebResponse)exception.Response).StatusCode);
                        Application.Current.Shutdown();
                    }
                    catch
                    {
                        MessageBox.Show("The program will now close!\r\nAn error occurred while connecting to the server");
                        Application.Current.Shutdown();
                    }
                }
                return null;

            }



        }
    }
}
