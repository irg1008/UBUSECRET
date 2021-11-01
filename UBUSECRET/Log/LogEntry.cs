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

    class LogEntry
    {
        private readonly Guid id;
        private Entry entry;
        private DateTime date;
        private User actor;
        private User affected;

        public LogEntry(Entry entry, User actor, User affected)
        {
            this.id = Guid.NewGuid();
            Entry = entry;
            Date = DateTime.Now;
            Actor = actor;
            Affected = affected;
        }

        public Guid Id => id;
        public Entry Entry { get => entry; set => entry = value; }
        public DateTime Date { get => date; set => date = value; }
        public User Actor { get => actor; set => actor = value; }
        public User Affected { get => affected; set => affected = value; }
    }
}
