using JX.Domain.Model.Entities;
using JX.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JX.Domain.IRepositories
{
    /// <summary>
    /// 表示实现该接口的类型是仓储上下文。
    /// </summary>
    public abstract class  RepositoryContext : DisposableObject,IRepositoryContext
    {
        private Guid id = Guid.NewGuid();
        private readonly ThreadLocal<bool> localCommitted = new ThreadLocal<bool>(() => true);

        /// <summary>
        /// 获取仓储上下文的ID。
        /// </summary>
        public Guid ID
        {
            get
            {
                return id;
            }
        }

        public RepositoryContext()
        {
        }

        /// <summary>
        /// 获得一个<see cref="System.Boolean"/>值，该值表示当前的Unit Of Work是否支持Microsoft分布式事务处理机制。
        /// </summary>
        public abstract bool DistributedTransactionSupported
        {
            get;
        }
        /// <summary>
        /// Gets a <see cref="System.Boolean"/> value which indicates whether the UnitOfWork
        /// was committed.
        /// </summary>
        public bool Committed
        {
            get
            {
                return localCommitted.Value;
            }
            protected set
            {
                localCommitted.Value = value;
            }
        }

        protected abstract void DoCommit();
        public virtual void Commit()
        {
            this.DoCommit();
        }

        public abstract void Rollback();

        /// <summary>
        /// 将指定的聚合根标注为“新建”状态。
        /// </summary>
        /// <typeparam name="TAggregateRoot">需要标注状态的聚合根类型。</typeparam>
        /// <param name="obj">需要标注状态的聚合根。</param>
        public abstract void RegisterNew<TAggregateRoot>(TAggregateRoot aggregateRoot) 
            where TAggregateRoot :class,IAggregateRoot;

        /// <summary>
        /// 将指定的聚合根标注为“更改”状态。
        /// </summary>
        /// <typeparam name="TAggregateRoot">需要标注状态的聚合根类型。</typeparam>
        /// <param name="obj">需要标注状态的聚合根。</param>
        public abstract void RegisterModified<TAggregateRoot>(TAggregateRoot obj)
            where TAggregateRoot : class, IAggregateRoot;

        /// <summary>
        /// 将指定的聚合根标注为“删除”状态。
        /// </summary>
        /// <typeparam name="TAggregateRoot">需要标注状态的聚合根类型。</typeparam>
        /// <param name="obj">需要标注状态的聚合根。</param>
        public abstract void RegisterDeleted<TAggregateRoot>(TAggregateRoot obj)
            where TAggregateRoot : class, IAggregateRoot;
    }
}
