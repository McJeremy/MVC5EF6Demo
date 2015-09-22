using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechPractise.EF;
using TechPractise.UnityAOP;
using TechPractise.规约模式;

namespace TechPractise
{
    class Program
    {
        static void Main(string[] args)
        {
            ////规约模式
            //SpecificationTestProgram stp = new SpecificationTestProgram();
            //stp.GetByName("xzz4").ToList().ForEach(u =>
            //{
            //    Console.WriteLine(string.Format("ID：{0},Name:{1}", u.ID, u.Name));
            //});

            ////EF
            //using (AccountContext ac=new AccountContext())
            //{
            //    Console.WriteLine(ac.Users.Count());
            //}

            //AOP
            ITalk talk = JX.Infrastructure.ServiceLocator.Instance.GetService<ITalk>();
            talk.CacheSay("A");
            talk.LogSay("B");

            Console.Read();
        }
    }
}
