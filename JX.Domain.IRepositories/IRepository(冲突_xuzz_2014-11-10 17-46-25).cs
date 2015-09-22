using JX.Domain.Model.Entities;
using JX.Domain.Specifications;
using JX.Infrastructure;
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
    public interface IRepository<TAggregateRoot> 
        where TAggregateRoot : class, IAggregateRoot
    {
        /// <summary>
        /// 获取当前仓储所使用的仓储上下文实例。        
        /// </summary>
        IRepositoryContext Context
        {
            get;
        }

        void Add(TAggregateRoot aggregateRoot);

        TAggregateRoot Get(Expression<Func<TAggregateRoot,bool>> wherePrecidate);

        TAggregateRoot GetByID(Guid id);

        IQueryable<TAggregateRoot> GetALL();

        //直接传入表达式方式
        IQueryable<TAggregateRoot> GetALL(Expression<Func<TAggregateRoot, bool>> wherePrecidate);
        PagedResult<TAggregateRoot> GetALL(Expression<Func<TAggregateRoot, bool>> wherePrecidate, int iPageSize, int iPageIndex);
        
        //使用规约方式
        IQueryable<TAggregateRoot> GetALL(ISpecification<TAggregateRoot> wherePrecidate);
        PagedResult<TAggregateRoot> GetALL(ISpecification<TAggregateRoot> wherePrecidate, int iPageSize, int iPageIndex);

        //直接传入表达式方式
        IQueryable<TAggregateRoot> GetALL(Expression<Func<TAggregateRoot, bool>> wherePrecidate, Expression<Func<TAggregateRoot, dynamic>> orderPrecidate,SortOrder order);
        PagedResult<TAggregateRoot> GetALL(Expression<Func<TAggregateRoot, bool>> wherePrecidate, Expression<Func<TAggregateRoot, dynamic>> orderPrecidate, SortOrder order, int iPageSize, int iPageIndex);

        //使用规约方式
        IQueryable<TAggregateRoot> GetALL(ISpecification<TAggregateRoot> wherePrecidate, Expression<Func<TAggregateRoot, dynamic>> orderPrecidate, SortOrder order);
        PagedResult<TAggregateRoot> GetALL(ISpecification<TAggregateRoot> wherePrecidate, Expression<Func<TAggregateRoot, dynamic>> orderPrecidate, SortOrder order, int iPageSize, int iPageIndex);

        /// <summary>
        /// 返回一个<see cref="Boolean"/>值，该值表示符合指定规约条件的聚合根是否存在。
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        bool Exists(ISpecification<TAggregateRoot> specification);

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
