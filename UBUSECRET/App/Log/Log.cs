using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    class Log
    {
        private List<LogEntry> logEntries;

        public Log()
        {
        }

        internal List<LogEntry> LogEntries { get => logEntries; set => logEntries = value; }

        public void addEntry(LogEntry newEntry)
        {
            LogEntries.Add(newEntry);
        }
    }
}
