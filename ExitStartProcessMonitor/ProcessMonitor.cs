using System;
using System.Management;

namespace ExitStartProcessMonitor
{
    public class ProcessMonitor
    {
        private const string ProcessName = "Code";
        private const string ProcessExe = "code.exe";

        private readonly string _eventQueryCondition = $"TargetInstance isa \"Win32_Process\" and TargetInstance.Name = '{ProcessExe}'";

        private readonly WindowsEventLogger _logger;

        public ProcessMonitor(WindowsEventLogger logger = null)
        {
            _logger = logger ?? new WindowsEventLogger();

            _logger.WriteToLog($"Watching for {ProcessName}");
        }
        
        public void MonitorProcessStart(EventArrivedEventHandler handler)
        {
            /*https://stackoverflow.com/questions/6575117/how-to-wait-for-process-that-will-be-started*/
            
            var watcher = new ManagementEventWatcher(
                new WqlEventQuery(
                    "__InstanceCreationEvent",
                    new TimeSpan(0, 0, 1),
                    _eventQueryCondition));

            watcher.Start();

            watcher.EventArrived += handler;
        }

        public void MonitorProcessExit(EventArrivedEventHandler handler)
        {
            var watcher = new ManagementEventWatcher(
                new WqlEventQuery(
                    "__InstanceDeletionEvent",
                    new TimeSpan(0, 0, 1),
                    _eventQueryCondition));

            watcher.Start();

            watcher.EventArrived += handler;
        }
    }
}
