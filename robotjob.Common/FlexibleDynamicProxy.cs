using System;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

namespace robotjob.Common
{

    public delegate void CustomeProxyEventHander<TEventArgs>(object sender, TEventArgs e, object obj);

    public sealed class FlexibleDynamicProxy : RealProxy
    {
        private Predicate<MethodInfo> _Filter;
        public Predicate<MethodInfo> Filter
        {
            get { return _Filter; }
            set
            {
                if (value == null)
                    _Filter = m => true;
                else
                    _Filter = value;
            }
        }
        public event CustomeProxyEventHander<IMethodCallMessage> BeforeExecute;
        public event CustomeProxyEventHander<IMethodCallMessage> AfterExecute;
        public event CustomeProxyEventHander<IMethodCallMessage> ErrorExecuting;
        public FlexibleDynamicProxy(Type type) :
            base(type)
        {
            _Filter = m => true;
        }
        private void OnBeforeExecute(IMethodCallMessage methodCall)
        {
            if (BeforeExecute != null)
            {
                var methodInfo = methodCall.MethodBase as MethodInfo;
                if (_Filter(methodInfo))
                    BeforeExecute(this, methodCall,null);
            }
        }
        private void OnAfterExecute(IMethodCallMessage methodCall, object returnValue = null)
        {
            if (AfterExecute != null)
            {
                var methodInfo = methodCall.MethodBase as MethodInfo;
                if (_Filter(methodInfo))
                    AfterExecute(this, methodCall, returnValue);
            }
        }
        private void OnErrorExecuting(IMethodCallMessage methodCall, string innerException)
        {
            if (ErrorExecuting != null)
            {
                var methodInfo = methodCall.MethodBase as MethodInfo;
                if (_Filter(methodInfo))
                    ErrorExecuting(this, methodCall, innerException);
            }
        }


        private static object obj = new object();
        //调用代理的类型的方法
        public override IMessage Invoke(IMessage msg)
        {
            //用lock来防止多线程在一个时间同时操作方法
            //lock (obj)
            //{

            //}

            object returnIMessage = null;
            IMessage message;
            //如果是调用了构造函数就进入使用下面的代码 
            if (msg is IConstructionCallMessage)
            {
                //构造函数调用
                IConstructionCallMessage constructCallMsg = msg as IConstructionCallMessage;
                IConstructionReturnMessage constructionReturnMessage = this.InitializeServerObject((IConstructionCallMessage)msg);
                RealProxy.SetStubData(this, constructionReturnMessage.ReturnValue);
                returnIMessage = constructionReturnMessage;

            }
            //如果调用方法是一般方法，进入If语句
            else
            {

                var methodCall = msg as IMethodCallMessage;
                var methodInfo = methodCall.MethodBase as MethodInfo;
                //if(_Filter(methodInfo))
                //    Log("In Dynamic Proxy - Before executing '{0}'", methodCall.MethodName);
                OnBeforeExecute(methodCall);
                try
                {
                    var result = methodInfo.Invoke(GetUnwrappedServer(), methodCall.InArgs);
                    //if (_Filter(methodInfo))
                    //    Log("In Dynamic Proxy - After executing '{0}' ", methodCall.MethodName);
                    message = new ReturnMessage(result, null, 0,
        methodCall.LogicalCallContext, methodCall);
                    OnAfterExecute(methodCall, message.Properties["__Return"]);
                    returnIMessage = message;
                }
                catch (Exception ex)
                {
                    //             if (_Filter(methodInfo))
                    //                 Log(string.Format(
                    //"In Dynamic Proxy- Exception {0} executing '{1}'", ex),
                    //methodCall.MethodName);
                    OnErrorExecuting(methodCall, ex.InnerException.ToString());
                    returnIMessage = new ReturnMessage(ex, methodCall);
                }
            }
            return returnIMessage as IMessage;
        }
    }
}
