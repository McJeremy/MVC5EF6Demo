using JX.Domain.IRepositories;
using JX.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JX.Infrastructure.Repositories.EntityFramework
{
    public class BlogRepository :EFRepositoryBase<Blog>,IBlogRepository
    {
        public BlogRepository(IRepositoryContext context) 
            :base(context)
        {
        }
    }
}
