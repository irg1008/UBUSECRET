﻿using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    public enum Entry
    {
        LOG_IN,
        LOG_OUT,
        CREATE_SECRET,
        DELETE_SECRET,
        DETACH_SECRET,
        DETACH_FROM_SECRET
    }

    class LogEntry
    {
        private readonly Guid id;
        private Entry entry;
        private DateTime date;
        private User actor;

        public LogEntry(Entry entry, DateTime date, User actor)
        {
            this.id = new Guid();
            Entry = entry;
            Date = date;
            Actor = actor;
        }

        public Guid Id => id;
        public Entry Entry { get => entry; set => entry = value; }
        public DateTime Date { get => date; set => date = value; }
        public User Actor { get => actor; set => actor = value; }
    }
}