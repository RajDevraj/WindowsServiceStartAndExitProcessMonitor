using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace ExitStartProcessMonitor
{
    [RunInstaller(true)]
    public class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            ServiceProcessInstaller serviceProcessInstaller = new ServiceProcessInstaller();
            ServiceInstaller serviceInstaller = new ServiceInstaller();

            serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            serviceProcessInstaller.Password = null;
            serviceProcessInstaller.Username = null;

            serviceInstaller.ServiceName = "ExitStartProcessMonitor";
            serviceInstaller.Description = "Service monitors a Process checking for its start and exit";
            serviceInstaller.DisplayName = "Exit Start Process Monitor";

            Installers.AddRange(new Installer[]
            {
                serviceProcessInstaller,
                serviceInstaller
            });
        }
    }
}