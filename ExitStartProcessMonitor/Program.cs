using System.ServiceProcess;

namespace ExitStartProcessMonitor
{
    class Program
    {
        static void Main()
        {
            ServiceBase.Run(new ExitStartService());
        }
    }
}
