using System;
using System.Collections.Generic;
using System.Text;

namespace Log
{
    class Log
    {
        private List<LogEntry> logEntries;

        public Log()
        {
        }

        internal List<LogEntry> LogEntries { get => logEntries; set => logEntries = value; }

        public void AddEntry(LogEntry newEntry)
        {
            LogEntries.Add(newEntry);
        }
    }
}
