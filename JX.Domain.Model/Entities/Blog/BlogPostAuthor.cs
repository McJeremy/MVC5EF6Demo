using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JX.Domain.Model.Entities
{
    /// <summary>
    /// 文章作者，与BlogPost存在多对多系(一篇文章有多个作者，一个作者有多篇文章）
    /// </summary>
    public class BlogPostAuthor:AggregateRoot
    {
        public string Name
        {
            get;set;
        }

        public virtual List<BlogPost> BlogPosts
        {
            get;set;
        }
    }
}
