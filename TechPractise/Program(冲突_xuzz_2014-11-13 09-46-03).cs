using JX.Infrastructure.Repositories.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            //ITalk talk = JX.Infrastructure.ServiceLocator.Instance.GetService<ITalk>();
            //talk.CacheSay("A");
            //talk.LogSay("B");

            //repository
            //RepositoryTest.RepositoryTestCenter.Center.InitUser();
            //RepositoryTest.RepositoryTestCenter.Center.ShowCount();
            //RepositoryTest.RepositoryTestCenter.Center.GetUserByID();
            //RepositoryTest.RepositoryTestCenter.Center.GetUserAll();
            //RepositoryTest.RepositoryTestCenter.Center.GetUserAllWithWhere();
            //RepositoryTest.RepositoryTestCenter.Center.GetUserWithSpecification();
            //RepositoryTest.RepositoryTestCenter.Center.GetUserAllWithSpecification();
            RepositoryTest.RepositoryTestCenter.Center.GetUserAllWithPaging();

            //RepositoryTest.RepositoryTestCenter.Center.InitBlog();
            //RepositoryTest.RepositoryTestCenter.Center.GetPostsOfBlog();

            //using (DDDTestContext tc = new DDDTestContext())
            //{
            //    var q = from p in tc.Blogs
            //            where p.Title == "Xuzzs Blog"
            //            select p;
            //    Console.WriteLine(q.ToString());
            //}

            Console.Read();
        }
    }
}
