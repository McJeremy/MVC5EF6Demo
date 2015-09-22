using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TechPractise.规约模式
{
    public class SpecificationTestProgram
    {
        private List<User> users = new List<User>();

        public SpecificationTestProgram()
        {
            for (int i = 1;i <= 10;i++)

            {

                User u = new User()

                {

                    ID = i ,
                    
                    Name = "xzz" + i.ToString()

                };

                users.Add(u);
            }
        }

        /// <summary>
        /// 用于测
        /// </summary>
        public IEnumerable<User> GetByName(string strName)
        {
            ISpecification<User> une2 = new UserNameEqualsSpecification(strName)
                                            .Or(new UserNameEqualsSpecification("xzz2"))
                                                .Or(new UserNameEqualsSpecification("xzz7"));
            //Console.WriteLine(une2.GetExpression().ToString());
            //users.Where(une.GetExpression().Compile())            

            return users.Where(une2.IsSatisfiedBy);
            
            
        }
    }
}
