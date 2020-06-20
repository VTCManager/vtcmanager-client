using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VTCManager.Klassen
{
    class FileHandler
    {
        public FileStream JobCacheFileStream;



        public static void kopiere_dateien()
        {
            string sourceFile = AppDomain.CurrentDomain.BaseDirectory + @"Icons\Wallpaper1.png";
            string destinationFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\VTCManager\Wallpaper1.png";
            try
            {
                File.Copy(sourceFile.ToString(), destinationFile, true);
            }
            catch (IOException iox)
            {
                MessageBox.Show(iox.Message);
            }
        }

        public bool create(string FilePath)
                {
                    try
                    {
                        JobCacheFileStream = File.Create(FilePath);
                        return true;
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        MessageBox.Show("No permissions to create " + FilePath + ex.Message,"ERROR",MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
        }



        internal static void StarteAnwendung(string path)
        {
            try
            {
                Process.Start(path);
                Logging.WriteClientLog("<INFO> Anwendung gestartet: " + path);
            }
            catch (Exception ex)
            {
                Logging.WriteClientLog("<ERROR> Konnte Anwendung in Pfad: " + path + " nicht starten. " + ex.Message + ex.StackTrace);
            }
        }

    }
}
