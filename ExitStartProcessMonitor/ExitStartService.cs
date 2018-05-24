using System;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Text;
using Newtonsoft.Json;

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
            _processMonitor.MonitorProcessExit(ExitEventHandler);
        }

        protected override void OnStop()
        {
            _logger.WriteToLog("Service closed");
        }

        private void ExitEventHandler(object sender, EventArgs args)
        {
            if (Process.GetProcessesByName("Code").Length == 0)
            {
                var file = File.Open(@"C:\Users\Rastapenguin\AppData\Roaming\Code\User\settings.json", FileMode.Open);

                var bytes = new BinaryReader(file).ReadBytes((int)file.Length);

                file.Close();

                var data = JsonConvert.DeserializeObject<dynamic>(Encoding.UTF8.GetString(bytes));

                if (data["editor.fontFamily"] != null && data["editor.fontFamily"] == "Comic Sans") return;

                data["editor.fontFamily"] = "Comic Sans";

                File.WriteAllText(@"C:\Users\Rastapenguin\AppData\Roaming\Code\User\settings.json", data.ToString());

                _logger.WriteToLog("Edited Settings Json");
            }
        }
    }
}
