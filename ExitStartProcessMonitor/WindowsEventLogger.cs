using System.Diagnostics;

namespace ExitStartProcessMonitor
{
    public class WindowsEventLogger
    {
        private readonly EventLog _eventLog;
        private const string EventSourceName = "ExitStartProcessMonitorSource";
        private const string EventLogName = "ExitStartProcessMonitorLog";

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
