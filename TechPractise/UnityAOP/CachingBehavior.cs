using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TechPractise.UnityAOP
{
    public class CachingBehavior : IInterceptionBehavior
    {
        public bool WillExecute
        {
            get
            {
                return true;
            }
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            var method = input.MethodBase;

            if (method.IsDefined(typeof(CacheAttribute), false))
            {
                //var cachingAttribute = (CachingAttribute)method.GetCustomAttributes(typeof(CachingAttribute), false)[0];
                //var arguments = new object[input.Arguments.Count];
                //input.Arguments.CopyTo(arguments, 0);
                //return new VirtualMethodReturn(input, obj, arguments);
                Console.WriteLine("Cache Interception");
            }

            return getNext().Invoke(input, getNext);
        }
    }
}
