using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace Xrf.Logging
{
    public class Log
    {
        public StringBuilder Main { get; private set; }

        public Log()
        {
           Main = new StringBuilder();
        }

        public void Write(string message)
        {
            string template = "[{0}] - {1}";
            string logEntry = string.Format(template, DateTime.Now.ToShortTimeString(), message);
            Main.AppendLine(logEntry);
        }
    }
}
