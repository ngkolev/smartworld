using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public sealed class FileLogger
    {
        private const string LOG_FILE_NAME = "smart_world_log.txt";
        private static readonly FileLogger current = new FileLogger();

        private FileLogger()
        {
            Log("Application started: {0:dd.MM.yy H:mm:ss}".Formatted(DateTime.Now));
        }

        public static FileLogger Current { get { return current; } }

        public void Log(string message)
        {
            lock (this)
            {
                using (var writer = new StreamWriter(LOG_FILE_NAME, true))
                {
                    writer.WriteLine(message);
                }
            }
        }
    }
}
