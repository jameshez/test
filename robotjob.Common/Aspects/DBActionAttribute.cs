using PostSharp.Aspects;
using System;

namespace robotjob.Common.Aspects
{
    internal class DBActionAttribute : OnMethodBoundaryAspect, IInstanceScopedAspect
    {
        public object CreateInstance(AdviceArgs adviceArgs)
        {
            throw new NotImplementedException();
        }

        public void RuntimeInitializeInstance()
        {
            throw new NotImplementedException();
        }
    }
}