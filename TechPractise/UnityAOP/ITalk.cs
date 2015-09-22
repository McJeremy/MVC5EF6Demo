using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechPractise.UnityAOP
{
    public interface ITalk
    {
        [Cache]
        void CacheSay(string strMsg);
        [Log]
        void LogSay(string strMsg);
    }
}
