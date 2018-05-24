using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExitStartProcessMonitor
{
    public class WindowsEventLogger
    {
        private readonly EventLog _eventLog;
        private const string EventSourceName = "TestServiceSource";
        private const string EventLogName = "TestServiceLog";

        public WindowsEventLogger(EventLog eventLog = null)
        {
            if (eventLog == null)
                _eventLog = new EventLog
                {
                    Source = EventSourceName,
                    Log = EventLogName
                };

            if (!EventLog.SourceExists(EventSourceName))
                EventLog.CreateEventSource(EventSourceName, EventLogName);
        }

        public void WriteToLog(string msg, EventLogEntryType entryType = EventLogEntryType.Information)
        {
            _eventLog.WriteEntry(msg, entryType);
        }
    }
}
