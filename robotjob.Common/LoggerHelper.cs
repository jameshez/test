using System;
using System.IO;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace robotjob.Common
{
    public class LoggerHelper
    {
        private static readonly string _errLogFilePath = @"C:\Users\James\Desktop\测试\log.txt";

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t">type</param>
        /// <param name="ex">exception</param>
        /// <author>hqq</author>
        public static void WriteErrorLog(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error("Error", ex);
        }
        public static void WriteErrorLog(Type t, string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error(msg);
        }



        public static void Writelog(string message)
        {
            StreamWriter sw = new StreamWriter(_errLogFilePath, true);
            String logContent = String.Format("[{0}]{1}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), message);
            sw.WriteLine(logContent);
            sw.Flush();
            sw.Close();
        }


    }
}
