using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ServerMonitor
{
    class EventlogHelper
    {
        public static void logEvent(String message)
        {
            EventLog.WriteEntry("ReachPlus Server Monitoring Service", message);
        }
    }
}
