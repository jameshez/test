using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using robotjob.Common;

namespace robotjob.Common.Aspects
{
    [Serializable]
    public class LogsAttribute : OnMethodBoundaryAspect
    {
        //进入函数时输出函数的输入参数
        public override void OnEntry(MethodExecutionArgs eventArgs)
        {
            LoggerHelper.Writelog(eventArgs.Arguments.ToString());
        }
    }
}
