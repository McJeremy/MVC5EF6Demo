using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TechPractise.规约模式
{
    /// <summary>
    /// 比对用户姓名是否相等
    /// </summary>
    public class UserNameEqualsSpecification : Specification<User>
    {
        private string _name;
        public UserNameEqualsSpecification(string strName)
        {
            this._name = strName;
        }

        public override Expression<Func<User, bool>> GetExpression()
        {
            return u => u.Name == _name;
        }
    }
}
