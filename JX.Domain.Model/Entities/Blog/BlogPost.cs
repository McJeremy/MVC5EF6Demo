using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JX.Domain.Model.Entities
{
    /// <summary>
    /// 博客文章，与Blog存在一对多关系(一个博客有多篇文章，多篇文章属于一个博客）
    /// </summary>
    public class BlogPost :AggregateRoot
    {
        public string Title
        {
            get;set;
        }

        public DateTime CreateDate
        {
            get;set;
        }

        public virtual Blog Blog
        {
            get;set;
        }

        public virtual List<BlogPostAuthor> BlogPostAuthors
        {
            get;set;
        }
    }
}
