using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace ServerMonitor
{
    [RunInstaller(true)]
    public class ServerMonitoringInstaller : Installer
    {
        /// <summary>
        /// </summary>
        public ServerMonitoringInstaller()
        {
            ServiceProcessInstaller serverMonitorServiceInstaller = new ServiceProcessInstaller();
            ServiceInstaller serviceInstaller = new ServiceInstaller();

            serverMonitorServiceInstaller.Account = ServiceAccount.LocalSystem;
            serverMonitorServiceInstaller.Username = null;
            serverMonitorServiceInstaller.Password = null;

            serviceInstaller.DisplayName = "ReachPlus Alerts Server Monitor";
            serviceInstaller.StartType = ServiceStartMode.Automatic;
            serviceInstaller.ServiceName = "ReachPlus Server Monitoring Service";
            this.Installers.Add(serverMonitorServiceInstaller);
            this.Installers.Add(serviceInstaller);
        }
    }
}
