using System;
using System.IO;

namespace robotjob.Common.Log
{
    public class LoggerHelper
    {
        /// <summary>
        /// 此处可以引入配置文件
        /// </summary>
        private static readonly string _logFolderPath = @"C:\Users\James\Desktop\测试\";

        private static readonly string _successLogFilePath = "logSuccess";
        private static readonly string _errLogFilePath = "logError";
        private static readonly string _warningLogFilePath = "logWarning";

        private static readonly string _suffix = ".txt";


        ///// <summary>
        ///// 输出日志到Log4Net
        ///// </summary>
        ///// <param name="t">type</param>
        ///// <param name="ex">exception</param>
        ///// <author>hqq</author>
        //public static void WriteErrorLog(Type t, Exception ex)
        //{
        //    log4net.ILog log = log4net.LogManager.GetLogger(t);
        //    log.Error("Error", ex);
        //}
        //public static void WriteErrorLog(Type t, string msg)
        //{
        //    log4net.ILog log = log4net.LogManager.GetLogger(t);
        //    log.Error(msg);
        //}



        public static void Writelog(string message, LogLevel level = LogLevel.Success)
        {
            string _logPath = _logFolderPath + DateTime.Now.ToString("yyyyMMdd");
            switch (level)
            {
                case LogLevel.Error:
                    _logPath += _errLogFilePath;
                    break;
                case LogLevel.Warning:
                    _logPath += _warningLogFilePath;
                    break;
                case LogLevel.Success:
                    _logPath += _successLogFilePath;
                    break;
                default:
                    _logPath += _successLogFilePath;
                    break;
            }
            _logPath = _logPath + _suffix;
            StreamWriter sw = new StreamWriter(_logPath, true);
            String logContent = String.Format("[{0}]{1}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), message);
            sw.WriteLine(logContent);
            sw.Flush();
            sw.Close();
        }


    }
}
