using JX.Domain.Model.Entities;
using JX.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JX.Domain.IRepositories
{
    /// <summary>
    /// 表示实现该接口的类型是仓储上下文。
    /// </summary>
    public interface IRepositoryContext : IUnitOfWork
    {
        /// <summary>
        /// 获取仓储上下文的ID。
        /// </summary>
        Guid ID
        {
            get;
        }

        /*
        思考，是否可以把下面的删除、修改、插入操作直接放到仓储超类中实现呢？
        至少在EF的实现下，没感觉到放在上下文中实现的好处？
        如下面是在上下文中实现的仓储超类：
        public class EFRepositoryBase<TAggregateRoot> : RepositoryBase<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
        {
            //通过依赖注入提供上下文实例
            private IEFRepositoryContext efContext;
            public EFRepositoryBase(IRepositoryContext context) :base(context)
            {
                if (context is IEFRepositoryContext)
                    efContext = context as IEFRepositoryContext;
            }

            protected IEFRepositoryContext EFContext
            {
                get
                {
                    return efContext;
                }
            }

            protected override void DoAdd(TAggregateRoot aggregateRoot)
            {
                efContext.RegisterNew<TAggregateRoot>(aggregateRoot);            
                //注意不要在这里提交。
                //需要在外面由efContext.DbContext.SaveChanges();
            }

         }

            如果直接在超类中实现呢？好像也没有什么不妥？

            所以我想，上下文只需要持有持久化的IUnitOfWork实现，及对外提供持久化上下文(DbContext)的引用即可？
            protected override void DoAdd(TAggregateRoot aggregateRoot)
            {
                //efContext.RegisterNew<TAggregateRoot>(aggregateRoot);  

                efContext.DbContext.Set<TAggregateRoot>().Add(aggregateRoot);
                          
                //注意不要在这里提交。
                //需要在外面由efContext.DbContext.SaveChanges();
            }
        */

        /// <summary>
        /// 将指定的聚合根标注为“新建”状态。
        /// </summary>
        /// <typeparam name="TAggregateRoot">需要标注状态的聚合根类型。</typeparam>
        /// <param name="obj">需要标注状态的聚合根。</param>
        void RegisterNew<TAggregateRoot>(TAggregateRoot aggregateRoot) 
            where TAggregateRoot :class,IAggregateRoot;

        /// <summary>
        /// 将指定的聚合根标注为“更改”状态。
        /// </summary>
        /// <typeparam name="TAggregateRoot">需要标注状态的聚合根类型。</typeparam>
        /// <param name="obj">需要标注状态的聚合根。</param>
        void RegisterModified<TAggregateRoot>(TAggregateRoot obj)
            where TAggregateRoot : class, IAggregateRoot;

        /// <summary>
        /// 将指定的聚合根标注为“删除”状态。
        /// </summary>
        /// <typeparam name="TAggregateRoot">需要标注状态的聚合根类型。</typeparam>
        /// <param name="obj">需要标注状态的聚合根。</param>
        void RegisterDeleted<TAggregateRoot>(TAggregateRoot obj)
            where TAggregateRoot : class, IAggregateRoot;
    }
}
