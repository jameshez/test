using System;


[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace robotjob.Common
{
    public class LoggerHelper
    {
        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t">type</param>
        /// <param name="ex">exception</param>
        /// <author>hqq</author>
        public static void WriteLog(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error("Error", ex);
        }

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t">type</param>
        /// <param name="msg">message</param>
        public static void WriteLog(Type t, string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error(msg);
        }



        public static int ADD(int x, int y)
        {
            return x+y;
        }

    }
}
