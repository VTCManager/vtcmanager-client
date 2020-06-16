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
