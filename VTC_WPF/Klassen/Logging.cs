using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace VTC_WPF.Klassen
{
    class Logging
    {
       
        public static void Make_Log_File()
        {
            if (!Directory.Exists(Config.LogRoot))
                Directory.CreateDirectory(Config.LogRoot);

            if (!File.Exists(Config.LogRoot + Config.ClientLogFileName))
                File.Create(Config.LogRoot + Config.ClientLogFileName); File.WriteAllText(Config.LogRoot + Config.ClientLogFileName, String.Empty);

            if (!File.Exists(Config.LogRoot + Config.SystemLogFileName))
                File.Create(Config.LogRoot + Config.SystemLogFileName); File.WriteAllText(Config.LogRoot + Config.SystemLogFileName, String.Empty);

        }

        public static void WriteClientLog(string text, [CallerLineNumber] int linenumber = 0, [CallerFilePath] string file = null)
        {
            if(File.Exists(Config.LogRoot + Config.ClientLogFileName))
                File.AppendAllText(Config.LogRoot + Config.ClientLogFileName, "<" + DateTime.Now + "> " + text + ", Line Number: " + linenumber + ", File: " + file + Environment.NewLine);
        }


    }
}
