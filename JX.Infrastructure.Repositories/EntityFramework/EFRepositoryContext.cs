using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JX.Domain.IRepositories;
using System.Threading;

namespace JX.Infrastructure.Repositories.EntityFramework
{
    /*
    注意在IOC注册的时候，要使用单例生命周期管理，不然两个Repository使用的就不是同一个上下文了。
    不使用的时候需要调用各自的，如：
            IBlogRepository blog = ServiceLocator.Instance.GetService<IBlogRepository>();
            IBlogUserRepository blogU = ServiceLocator.Instance.GetService<IBlogUserRepository>();
            ..........................
            blog.Context.Commit();
            blogU.Context.Commit();
    使用了单例的时候，只需要一次，如：
            IBlogRepository blog = ServiceLocator.Instance.GetService<IBlogRepository>();
            IBlogUserRepository blogU = ServiceLocator.Instance.GetService<IBlogUserRepository>();
            ..........................
            blog.Context.Commit();
    */

    public class DDDTestRepositoryContext : EFRepositoryContext<DDDTestContext>
    {
        //如果有多个数据库，那每个数据库都需要自己的上下文
    }

    public class EFRepositoryContext<TDbContext> : RepositoryContext,IEFRepositoryContext
        where TDbContext :DbContext
    {
        private readonly ThreadLocal<DDDTestContext> localDbCtx = new ThreadLocal<DDDTestContext>(() =>
        {
            return new DDDTestContext();
        });
        

        /// <summary>
        /// 数据库上下文
        /// </summary>
        public DbContext DbContext
        {
            get
            {
                return localDbCtx.Value;                
            }
        }

        public override bool DistributedTransactionSupported
        {
            get
            {                
                return true;
            }
        }

        public override void RegisterDeleted<TAggregateRoot>(TAggregateRoot obj)
        {
            localDbCtx.Value.Set<TAggregateRoot>().Remove(obj);
            this.Committed = false;
        }

        public override void RegisterModified<TAggregateRoot>(TAggregateRoot obj)
        {
            localDbCtx.Value.Entry<TAggregateRoot>(obj).State = EntityState.Modified;
            this.Committed = false;
        }

        public override void RegisterNew<TAggregateRoot>(TAggregateRoot aggregateRoot)
        {
            localDbCtx.Value.Set<TAggregateRoot>().Add(aggregateRoot);
            this.Committed = false;
        }

        public override void Rollback()
        {
            this.Committed = false;
        }

        protected override void DoCommit()
        {
            if (!this.Committed)
            {
                //var errors = localDbCtx.Value.GetValidationErrors();
                var count = localDbCtx.Value.SaveChanges();
                this.Committed = true;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!Committed)
                    Commit();
                localDbCtx.Value.Dispose();
                localDbCtx.Dispose();
                base.Dispose(disposing);
            }
        }
    }
}
