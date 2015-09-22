using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JX.Domain.Model.Entities
{
    /// <summary>
    /// 博客
    /// </summary>
    public class Blog :AggregateRoot
    {
        public string Title
        {
            get;set;
        }

        //这样写，无论是否延迟加载，在生成SQL语句的时候，都会JOIN BlogUser，
        //如果只需要
        public virtual BlogUser BlogUser
        {
            get; set;
        }

        public virtual List<BlogPost> BlogPosts
        {
            get;set;
        }
    }
}
