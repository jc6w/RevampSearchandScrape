using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using Newtonsoft.Json;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.PolicyInjection.Pipeline;

namespace RevampSearchandScrape
{
    public class LoggingInterceptor: IInterceptionBehavior
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private delegate void logPriorMethodCallDelegate(IMethodInvocation input);
        private delegate void logAfterMethodCallDelegate(IMethodInvocation input, IMethodReturn methodReturn, Stopwatch stopwatch);
        public LoggingInterceptor()
        {

        }

        public Unity.Interception.PolicyInjection.Pipeline.IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            logPriorMethodCallDelegate priorMethodCallObject = new logPriorMethodCallDelegate(LogPriorMethodCall);
            logAfterMethodCallDelegate afterMethodCallObject = new logAfterMethodCallDelegate(LogAfterMethodCall);


            // Code here will execute prior to the method that was called.

            /*Every time a method that is going to be intercepted is invoked, 
             instead of triggering the method,  Unity calls this Invoke method. 
             Input parameter (IMethodInvocation) contains all the information about 
             the method such as it’s name, it’s parameters.*/

            Task PriorMethodCall = Task.Factory.StartNew(() => priorMethodCallObject(input));
            PriorMethodCall.Wait();

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Invoke the next behavior in the chain.
            //This GetNext is a delegate holding a pointer to the actual method should be called.
            var methodReturn = getNext().Invoke(input, getNext);

            // Code here will execute after the method that was called.
            stopwatch.Stop();

            Task AfterMethodCall = Task.Factory.StartNew(() => afterMethodCallObject(input, methodReturn, stopwatch));
            AfterMethodCall.Wait();

            return methodReturn;
        }


        public IEnumerable<Type> GetRequiredInterfaces()
        {
            //This method returns the interfaces required by the behavior for the intercepted objects.
            //logger.Info(String.Format("[{0}:{1}]", this.GetType().Name, "GetRequiredInterfaces"));
            return Type.EmptyTypes;
        }

        public bool WillExecute
        {
            //This property is used to optimize proxy creation. It simply returns a flag indicating if the behavior will actually do anything when invoked, and if not, enables the interception mechanism to skip the behavior
            get
            {
                //logger.Info(String.Format("[{0}:{1}]", this.GetType().Name, "WillExecute"));
                return true;
            }
        }

        private void LogPriorMethodCall(IMethodInvocation input)
        {
            logger.Info(String.Format("Method Invoked: {0}", input.MethodBase.Name));

            for (int i = 0; i < input.Inputs.Count; i++)
            {
                var type = input.Inputs.GetType();
                logger.Info(String.Format("Inputs Data:  {0} : {1} ", input.Inputs.ParameterName(i), JsonConvert.SerializeObject(input.Inputs[i])));
            }
        }

        private void LogAfterMethodCall(IMethodInvocation input, IMethodReturn methodReturn, Stopwatch stopwatch)
        {
            if (methodReturn.Exception != null)
            {
                logger.Info(methodReturn.ReturnValue?.ToString());
                Task.Factory.StartNew(() => logger.Error(String.Format("Method {0} threw exception {1} with stack trace {2} at {3}", input.MethodBase, methodReturn.Exception?.Message, methodReturn.Exception?.StackTrace, DateTime.Now.ToLongTimeString())));

                methodReturn.Exception.FlattenHierarchy().ToList().ForEach(innerEx => {
                    Task.Factory.StartNew(() => logger.Error(String.Format(" Method {0} threw exception {1} at {2}", input.MethodBase, innerEx.Message, DateTime.Now.ToLongTimeString())));
                });

            }
            else
            {
                logger.Info(String.Format("Method {0} returned {1} at {2}", input.MethodBase, methodReturn?.ReturnValue, DateTime.Now.ToLongTimeString()));
            }
        }
    }
}
