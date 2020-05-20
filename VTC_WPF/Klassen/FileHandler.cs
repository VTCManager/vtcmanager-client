using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VTC_WPF.Klassen
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
    }
}
