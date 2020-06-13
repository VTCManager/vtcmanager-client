using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTCManager.Objects;

namespace VTCManager.Klassen
{
    public class JobHandler
    {
        private FileHandler Files;
        public List<Job> JobsList;
        public JobHandler()
        {
            Files = new FileHandler();
            if (!File.Exists(Config.JobsCache))
            {
                Files.create(Config.JobsCache);
            }

        }
    }
}
