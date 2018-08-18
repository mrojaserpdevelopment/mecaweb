using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace KlarnaWebAPI.Errors
{
    public class LogHandler
    {
        static EventLog eventLog = new EventLog("Application");
        
        public static void LogException(string message)
        {
            eventLog.Source = "Application";
            eventLog.WriteEntry(message, EventLogEntryType.Information, 101, 1);
        }
        public static void LogException(DbEntityValidationException e)
        {            
            //EventLog eventLog = new EventLog("Application");
            eventLog.Source = "Application";
            eventLog.WriteEntry("EntityValidationErrors", EventLogEntryType.Information, 101, 1);
            foreach (var eve in e.EntityValidationErrors)
            {
                eventLog.WriteEntry("ValidationErrors", EventLogEntryType.Information, 101, 1);
                foreach (var ve in eve.ValidationErrors)
                {
                    eventLog.WriteEntry("Property: " + ve.PropertyName + " - " + "Error: " + ve.ErrorMessage, EventLogEntryType.Information, 101, 1);
                }
            }
        }
    }
}