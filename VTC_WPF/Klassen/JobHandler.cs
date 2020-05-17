using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTC_WPF.Klassen
{
    public class JobHandler
    {
        private FileStream JobCacheFileStream;
        private FileHandler Files;
        public JobHandler()
        {

            if (!File.Exists(Config.JobsCache))
            {
                Files.create(Config.JobsCache);
            }
        }
    }
}
