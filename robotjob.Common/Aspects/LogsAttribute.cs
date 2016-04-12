using PostSharp.Aspects;
using System;
using System.Diagnostics;

namespace robotjob.Common.Aspects
{
    /// <summary>
    /// 目前记录了方法的执行时间
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
            base.OnEntry(eventArgs);
            LoggerHelper.Writelog("方法 " + eventArgs.Method.Name + " 开始执行");
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            _stopwatch.Stop();
            base.OnSuccess(args);
            LoggerHelper.Writelog("方法 " + args.Method.Name + " 执行结束，用时 " + _stopwatch.ElapsedMilliseconds + "ms" );
        }

        public void RuntimeInitializeInstance()
        {
            _stopwatch = new Stopwatch();
        }
    }
}
