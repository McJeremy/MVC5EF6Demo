using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechPractise.UnityAOP
{
    public class Talk :ITalk
    {
        public void CacheSay(string strMsg)
        {
            Console.WriteLine(strMsg);
        }

        public void LogSay(string strMsg)
        {
            Console.WriteLine(strMsg);
        }
    }
}
