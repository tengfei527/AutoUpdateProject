using System;
using System.Collections.Generic;
using System.Web;

namespace AU.Common.Utility
{
    public class Logger
    {
        public static void LogError(Exception exception, string message, string customerName)
        {
            try
            {
                string exceptionMessage = exception.Message;
                if (exception.InnerException != null)
                    exceptionMessage += "/r/n" + exception.InnerException.Message;

                SendToConsole(message, exceptionMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void LogInfo(string logAction, string logDetail, string customerName)
        {
            try
            {
                SendToConsole(logAction, logDetail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void SendToConsole(string logAction, string logDetail)
        {
            Console.WriteLine(string.Empty);
            //Console.WriteLine(string.Format("============= {0} ===============================================", DateTime.Now.ToString("HH:mm:ss:ff")));
            Console.WriteLine(string.Format("== {0} ==", DateTime.Now.ToString("HH:mm:ss:ff")));
            Console.WriteLine(string.Empty);
            Console.WriteLine(logAction);
            Console.WriteLine(logDetail);
        }
    }
}