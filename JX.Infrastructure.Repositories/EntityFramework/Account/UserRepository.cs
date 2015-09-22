using JX.Domain.IRepositories;
using JX.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JX.Infrastructure.Repositories.EntityFramework
{
    public class UserRepository :EFRepositoryBase<User>,IUserRepository
    {
        public UserRepository(IRepositoryContext context) 
            : base(context)
        {
        }
    }
}
