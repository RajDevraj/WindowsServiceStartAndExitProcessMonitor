using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace ExitStartProcessMonitor
{
    public class ProcessMonitor
    {
        private const string ProcessName = "notepad";
        private const string ProcessExe = "notepad.exe";

        private readonly WindowsEventLogger _logger;

        private ManagementEventWatcher _watcher;
        private Process _monitoredProcess;

        public ProcessMonitor(WindowsEventLogger logger = null)
        {
            _logger = logger ?? new WindowsEventLogger();
        }

        public void MonitorProcessStart(EventArrivedEventHandler handler)
        {
            /*https://stackoverflow.com/questions/6575117/how-to-wait-for-process-that-will-be-started*/

            _watcher = new ManagementEventWatcher(
                new WqlEventQuery(
                    "__InstanceCreationEvent",
                    new TimeSpan(0, 0, 1),
                    $"TargetInstance isa \"Win32_Process\" and TargetInstance.Name = '{ProcessExe}'"));

            _logger.WriteToLog($"Watching for {ProcessName}");

            _watcher.Start();

            _watcher.EventArrived += handler;
        }

        public void MonitorProcessExit(EventHandler handler)
        {
            try
            {
                _monitoredProcess = Process.GetProcesses().First(p => p.ProcessName == ProcessName);
                _monitoredProcess.EnableRaisingEvents = true;
            }
            catch (Exception)
            {
                _logger.WriteToLog($"{ProcessName} not found", EventLogEntryType.Error);
                throw;
            }

            _monitoredProcess.Exited += handler;
        }
    }
}
