using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarteSchlange.API.Models;

namespace WarteSchlange.API.Helpers
{
    public static class ExceptionHandler
    {
        public static void LogException(string desc, ErrorLevel errorLevel, MainContext context)
        {
            LogModel logEntry = new LogModel()
            {
                Description = desc,
                ErrorLevel = errorLevel,
                OccuredTime = DateTime.Now
            };

            context.Add(logEntry);

            try
            {
                context.SaveChanges();
            }
            catch
            {

            }
        }

        public enum ErrorLevel : int
        {
            INFO = 0,
            WARNING = 1,
            ERROR = 2,
            FATAL = 3
        }
    }
}
