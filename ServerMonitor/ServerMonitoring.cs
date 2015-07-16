using System;
using System.Configuration;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.ServiceProcess;
using System.Timers;

namespace ServerMonitor
{
    class ServerMonitoring : ServiceBase
    {
        const int TIMER_INTERVAL = 30 * 1000; // 30 seconds
        static Timer timer = new Timer();
        static Boolean alertTriggered = false;
        /// <summary>
        /// Constructor
        /// </summary>
        public ServerMonitoring()
        {
            
            this.CanHandlePowerEvent = true;
            this.CanHandleSessionChangeEvent = true;
            this.CanPauseAndContinue = true;
            this.CanShutdown = true;
            this.CanStop = true;

            this.ServiceName = "ReachPlus Server Monitoring Service";
            this.EventLog.Source = "ReachPlus Server Monitoring Service";
            this.EventLog.Log = "Application";

            if (!EventLog.SourceExists("ReachPlus Server Monitoring Service"))
                EventLog.CreateEventSource("ReachPlus Server Monitoring Service", "Application");
        }

        /// <summary>
        /// </summary>
        static void Main()
        {
            ServiceBase.Run(new ServerMonitoring());
        }

        /// <summary>
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        /// <summary>
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            timer.AutoReset = true;
            timer.Interval = TIMER_INTERVAL;
            timer.Elapsed += new ElapsedEventHandler(TimerCallback);
            timer.Enabled = true;
            timer.Start();
          
        }

        private static void TimerCallback(object sender, ElapsedEventArgs e)
        {
            if (alertTriggered)
                {
                    alertTriggered = false;
                    timer.Stop();
                    timer.Interval = TIMER_INTERVAL;
                    timer.Start();
                }
            else
                Pinger();

            GC.Collect();
        }

        private static void Pinger()
        {
            timer.Stop();
            System.Configuration.AppSettingsReader reader = new AppSettingsReader();
            var server = reader.GetValue("serveraddress", typeof(string)).ToString();
            Ping ping = new Ping();
            PingReply pingresult = ping.Send(server);
            if (pingresult.Status.ToString() != "Success")
            {
                timer.Interval = TimeSpan.FromMinutes(5).TotalMilliseconds;// change timer interval to 5 minutes
                timer.Start();
                alertTriggered = true;
                SDKHelper.sendAlert();
            }
        }
    }
}
