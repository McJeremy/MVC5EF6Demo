using JX.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JX.Domain.IRepositories
{
    /// <summary>
    /// 表示实现该接口的类型是应用于某种聚合根的仓储类型。
    /// </summary>
    /// <typeparam name="TAggregateRoot">聚合根类型。</typeparam>
    public interface IRepository<TAggregateRoot> where TAggregateRoot : class, IAggregateRoot
    {
        void Add(TAggregateRoot aggregateRoot);

        TAggregateRoot GetByID(Guid id);

        IEnumerable<TAggregateRoot> GetALL();

        //IEnumerable<TAggregateRoot> GetALL(Expression<Func<TAggregateRoot,bool>> wherePrecidate);

        ///// <summary>
        ///// 根据指定的规约，从仓储中获取所有符合条件的聚合根。
        ///// </summary>
        ///// <param name="specification">规约。</param>
        ///// <returns>所有符合条件的聚合根。</returns>
        //IEnumerable<TAggregateRoot> GetAll(ISpecification<TAggregateRoot> specification);

        /// <summary>
        /// 将指定的聚合根从仓储中移除。
        /// </summary>
        /// <param name="aggregateRoot">需要从仓储中移除的聚合根。</param>
        void Remove(TAggregateRoot aggregateRoot);

        /// <summary>
        /// 更新指定的聚合根。
        /// </summary>
        /// <param name="aggregateRoot">需要更新的聚合根。</param>
        void Update(TAggregateRoot aggregateRoot);
    }
}
