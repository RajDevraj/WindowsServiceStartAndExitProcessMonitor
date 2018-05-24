using System;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;

namespace ExitStartProcessMonitor
{
    public class ExitStartService : ServiceBase
    {
        private readonly WindowsEventLogger _logger;
        private readonly ProcessMonitor _processMonitor;

        public ExitStartService(ProcessMonitor processMonitor = null, WindowsEventLogger logger = null)
        {
            _processMonitor = processMonitor ?? new ProcessMonitor();
            _logger = logger ?? new WindowsEventLogger();
        }

        protected override void OnStart(string[] args)
        {
            _processMonitor.MonitorProcessStart(EventHandler);
        }

        protected override void OnStop()
        {
            _logger.WriteToLog("Service closed");
        }

        private void EventHandler(object s, EventArgs a)
        {
            _processMonitor.MonitorProcessExit((sender, args) =>
            {
                _logger.WriteToLog("Process Exited");
            });
        }
    }
}
