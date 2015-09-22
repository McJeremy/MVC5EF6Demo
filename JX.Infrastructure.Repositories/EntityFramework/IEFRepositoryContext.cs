using JX.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JX.Infrastructure.Repositories.EntityFramework
{
    public interface IEFRepositoryContext :IRepositoryContext
    {
        DbContext DbContext
        {
            get;
        }
    }
}
