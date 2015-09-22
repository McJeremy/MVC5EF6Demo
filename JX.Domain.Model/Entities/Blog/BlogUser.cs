using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JX.Domain.Model.Entities
{
    /// <summary>
    /// 博客主人，与Blog存在一对一关系(一个博客，只有一个用户，一个用户也只有一个博客）
    /// </summary>
    public class BlogUser :AggregateRoot
    {
        public string Name
        {
            get;set;
        }

        public virtual Blog Blog
        {
            get; set;
        }
    }
}
