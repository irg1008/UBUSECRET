using Microsoft.VisualStudio.TestTools.UnitTesting;
using Log;
using System;
using System.Collections.Generic;
using System.Text;
using Main;

namespace Log.Tests
{
    [TestClass()]
    public class LogEntryTests
    {
        [TestMethod()]
        public void LogEntryTest()
        {
            LogEntry entry = new LogEntry(Entry.ADD_CONSMER, "Adding new consumer");

            Assert.IsNotNull(entry.Id);
            Assert.IsInstanceOfType(entry.Id, typeof(Guid));

            Assert.IsNotNull(entry.Message);
            Assert.IsInstanceOfType(entry.Message, typeof(string));

            Assert.IsNotNull(entry.Date);
            Assert.IsInstanceOfType(entry.Date, typeof(DateTime));

            var timeFromEntry = (DateTime.Now - entry.Date).TotalMilliseconds;
            var threshold_ms = 200;
            var elapsedTimeIsCorrect = timeFromEntry < threshold_ms;

            Assert.IsTrue(elapsedTimeIsCorrect);

            // Check empty message.
            entry = new LogEntry(Entry.CREATE_INVITATION, "");
            Assert.AreEqual(entry.Message, "No further message provided");
        }
    }
}