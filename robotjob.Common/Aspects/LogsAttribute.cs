using PostSharp.Aspects;
using robotjob.Common.Log;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace robotjob.Common.Aspects
{
    /// <summary>
    /// 目前记录了方法的执行时间，输出样例：
    /// [2016-04-12 10:39:36]方法 GetUser 执行结束，用时 164ms
    /// </summary>
    [Serializable]
    public class LogsAttribute : OnMethodBoundaryAspect, IInstanceScopedAspect
    {
        [NonSerialized]
        private Stopwatch _stopwatch;

        public object CreateInstance(AdviceArgs adviceArgs)
        {
            return MemberwiseClone();
        }

        public override void OnEntry(MethodExecutionArgs eventArgs)
        {
            _stopwatch.Restart();
            Arguments arguments = eventArgs.Arguments;
            StringBuilder sb = new StringBuilder();
            ParameterInfo[] parameters = eventArgs.Method.GetParameters();
            for (int i = 0; arguments != null && i < arguments.Count; i++)
            {
                //进入的参数的值
                sb.Append(parameters[i].Name + "=" + arguments[i] + "");
            }
            LoggerHelper.Writelog("方法 " + eventArgs.Method.Name + " 开始执行，参数为：" + sb);
            
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            _stopwatch.Stop();
            LoggerHelper.Writelog(
                "方法 " + args.Method.Name + " 执行结束，用时 " + _stopwatch.ElapsedMilliseconds + "ms",
                LogLevel.Success
                );
        }

        public override void OnException(MethodExecutionArgs args)
        {
            _stopwatch.Stop();
            LoggerHelper.Writelog(
                "方法 " + args.Method.Name + " 执行出错，错误为：" + args.Exception,
                LogLevel.Error
                );
        }

        public void RuntimeInitializeInstance()
        {
            _stopwatch = new Stopwatch();
        }
    }
}
