using System;
using System.Collections.Generic;
using System.Text;
using Main;

namespace Log
{
    public enum Entry
    {
        LOG_IN,
        LOG_OUT,
        CREATE_SECRET,
        DELETE_SECRET,
        ADD_CONSMER,
        DETACH_CONSUMER,
        DETACH_FROM_SECRET,
        CREATE_INVITATION
    }


    public class LogEntry
    {
        private static readonly Dictionary<Entry, string> enumValues = new Dictionary<Entry, string>() {
            { Log.Entry.LOG_IN, "Log In" },
            { Log.Entry.LOG_OUT, "Log Out" },
            { Log.Entry.ADD_CONSMER, "New consumer" },
            { Log.Entry.DETACH_CONSUMER, "Owner detatched consumer" },
            { Log.Entry.DETACH_FROM_SECRET, "Consumer detatched itself from secret" },
            { Log.Entry.CREATE_SECRET, "New secret" },
            { Log.Entry.DELETE_SECRET, "Secret deleted" },
            { Log.Entry.CREATE_INVITATION, "New invitation" }
        };

        private readonly Guid id;
        private string entry;
        private DateTime date;
        private string message;

        public LogEntry(Entry entry, string message)
        {
            // Create static dictionary.

            this.id = Guid.NewGuid();
            Entry = Parse(entry);
            Date = DateTime.Now;
            Message = message == "" ? "No further message provided" : message;
        }

        private string Parse(Entry entry)
        {
            return enumValues[entry];
        }

        public Guid Id => id;
        public string Entry { get => entry; set => entry = value; }
        public DateTime Date { get => date; set => date = value; }
        public string Message { get => message; set => message = value; }
    }
}
