using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JX.Domain.IRepositories;
using JX.Domain.Model.Entities;

namespace JX.Infrastructure.Repositories
{
    //超类。它实现了IRepository中与具体实体无关的操作。而具体的实体仓储类只需要处理自己特别的操作
    public abstract class Repository<TAggregateRoot> : IRepository<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
    {
        //在这个基类中，通过泛型实现不特定实体（或值对象）的CRUD操作。
        //也可以在这个基类中持有仓储上下文以实现事务统一处理操作
        //具体在其它地方使用时，是用哪个持久化的仓储实现，可以通过显示生成实例。如：
        // IUserRepository user  = new JX.Infrastructure.Reporitories.MongoDB.UserRepository()；
        //也可以使用依赖注入的方式使用。

        //具体持久化仓储基类继承当前抽象类，然后具体的实体仓储又继承持久化仓储基类
        //而实体仓储在每一类型的持久化工具中，都有相应的文件(如：在MongoDB和EFDB下都有UserRepository类,通常这两个类没有啥特别的操作，因为常规的CRUD操作都在持久化仓储基类中实现了，比如EFRepository中实现了CRUD）

        //思考这样实现：
        /*
            ---不使用上下文的情况下
            public class EFRepository<TAggregateRoot> : Repository<TAggregateRoot>
                    where TAggregateRoot : class, IAggregateRoot
            {
                private readonly ThreadLocal<XXDbContext> localCtx = new ThreadLocal<XXDbContext>(() => new XXDbContext());
                protected override void DoAdd(TAggregateRoot aggregateRoot)
                {
                    localCtx.Value.Set<TAggregateRoot>().Add(aggregateRoot);
                    localCtx.Value.SaveChanges();
                }
            }

            public class RoleRepository : EFRepository<Role>, IRoleRepository
            {
            }
        */

        /*--使用上下文的情况下(注：由于是事务操作，所以上下文一般只处理更新、删除操作）
            --同时，上下文接口可以继承IUnitOfWork，以实现对一批操作进行统一提交
            public class EFRepository<TAggregateRoot> : Repository<TAggregateRoot>
                    where TAggregateRoot : class, IAggregateRoot
            {
                private readonly IEntityFrameworkRepositoryContext efContext;
                public EntityFrameworkRepository(IRepositoryContext context)
                    : base(context)
                {
                    if (context is IEntityFrameworkRepositoryContext)
                        this.efContext = context as IEntityFrameworkRepositoryContext;
                }

                //通过这个上下文，可以找到DBContext，
                protected IEntityFrameworkRepositoryContext EFContext
                {
                    get { return this.efContext; }
                }

                protected override void DoAdd(TAggregateRoot aggregateRoot)
                {
                    efContext.RegisterNew<TAggregateRoot>(aggregateRoot);
                    //此处实际并没有提交，需要用到IEntityFrameworkRepositoryContextd的Commit方法，而
                    //IEntityFrameworkRepositoryContextd的Commit方法是继承IUnitOfWork接口的
                }
            }

            public class RoleRepository : EFRepository<Role>, IRoleRepository
            {
                public RoleRepository(IRepositoryContext context)
                    : base(context)
                { }

            }
        */

        /*
        //在使用了上下文的情况下，可以在本类公开上下文，以用于提交等，如
        //IUserRepository user  = new JX.Infrastructure.Reporitories.MongoDB.UserRepository()；
        //user.Add(xxx);
        //user.Update(xxx);
        //user.Context.Commit();
         */
        private IRepositoryContext context;
        public IRepositoryContext Context
        {
            get
            {
                return this.context;
            }
        }
        public Repository(IRepositoryContext context)
        {
            this.context = context;
        }
   

        //定义一些抽象方法，让这些抽象方法在具体的持久化工具中实现持久化操作
        protected abstract void DoAdd(TAggregateRoot aggregateRoot);
        protected abstract IEnumerable<TAggregateRoot> DoGetALL();
        protected abstract TAggregateRoot DoGetByID(Guid id);
        protected abstract void DoRemove(TAggregateRoot aggregateRoot);
        protected abstract void DoUpdate(TAggregateRoot aggregateRoot);


        //一下的方法是公开使用的，他们内部调用了不同持久化工具的操作
        public void Add(TAggregateRoot aggregateRoot)
        {
            this.DoAdd(aggregateRoot);
        }

        public IEnumerable<TAggregateRoot> GetALL()
        {
            return this.DoGetALL();
        }

        public TAggregateRoot GetByID(Guid id)
        {
            return this.DoGetByID(id);
        }

        public void Remove(TAggregateRoot aggregateRoot)
        {
            this.DoRemove(aggregateRoot);
        }

        public void Update(TAggregateRoot aggregateRoot)
        {
            this.DoUpdate(aggregateRoot);
        }
    }
}
