using SharpDX.Multimedia;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine_Integrated_Karting_Experience
{
    internal class Utils
    {
        private const string SOURCE   = "M.I.K.E.";
        private const string LOG_NAME = "M.I.K.E.";

        private static void LogEvent(string message, EventLogEntryType type)
        {

            try
            {
                //if the source exists
                //do nothing
                EventLog.SourceExists(SOURCE);
            }
            catch
            {
                //else, create the source
                EventLog.CreateEventSource(SOURCE, LOG_NAME);
            }

            //log the event using the established eventlog
            using (EventLog eventLog = new EventLog(LOG_NAME))
            {
                eventLog.Source = SOURCE;
                eventLog.WriteEntry(message, type);
            }
        }

        public static void LogWarning(string message)
        {
            LogEvent(message, EventLogEntryType.Warning);
        }

        public static void LogInfo(string message)
        {
            LogEvent(message, EventLogEntryType.Information);
        }

        public static void LogError(string message)
        {
            LogEvent(message, EventLogEntryType.Error);
        }
    }
}
