using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
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
        public static string server = "http://localhost:8000/api/";
        public static string get_user = server + "user";

        public static JObject HTTPSRequestGet(string url, Dictionary<string, string> getParameters = null)
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
            request1.UserAgent = "VTCM "+Config.ClientVersion;
            request1.Accept = "application/json";
            request1.AutomaticDecompression = DecompressionMethods.GZip;
            request1.Headers.Add("Authorization", "Bearer " + Config.AccessToken);
            try{
                WebResponse response = request1.GetResponse();
                string str2 = null;
                using (Stream stream = response.GetResponseStream())
                {
                    str2 = new StreamReader(stream).ReadToEnd();
                }
                response.Close();
                Console.WriteLine(str2);
                JObject json = JObject.Parse(str2);
                return json;
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ex.Response as HttpWebResponse;
                    if (response != null)
                    {
                        String error_response = "{'error': true,'error_code': " + (int)response.StatusCode + ",}";
                        return JObject.Parse(error_response);
                    }
                    else
                    {
                        String error_response = "{'error': true,'error_code': 000,}";
                        return JObject.Parse(error_response);
                    }
                }
                else
                {
                    String error_response = "{'error': true,'error_code': 000,}";
                    return JObject.Parse(error_response);
                }
            }
        }
        public static JObject HTTPSRequestPost(string url, Dictionary<string, string> postParameters, bool outputError = true)
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

                // TODO Thommy: fehlende macAddr ??
                //Console.WriteLine(Config.macAddr); 
                JObject json = JObject.Parse(str3);
                return json;
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
