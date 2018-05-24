using System;
using System.Diagnostics;
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

        protected override void OnStart(string[] arguments)
        {
            _processMonitor.MonitorProcessStart((sender, args) => _logger.WriteToLog("Process Started"));
            _processMonitor.MonitorProcessExit((sender, args) => _logger.WriteToLog("Process Started"));
        }

        protected override void OnStop()
        {
            _logger.WriteToLog("Service closed");
        }
    }
}
