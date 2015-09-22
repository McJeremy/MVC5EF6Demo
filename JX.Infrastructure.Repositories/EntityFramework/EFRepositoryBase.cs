using JX.Domain.Model.Entities;
using JX.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JX.Domain.IRepositories;
using JX.Domain.Specifications;
using System.Linq.Expressions;

namespace JX.Infrastructure.Repositories.EntityFramework
{
    public class EFRepositoryBase<TAggregateRoot> : RepositoryBase<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
    {
        //通过依赖注入提供上下文实例
        private IEFRepositoryContext efContext;
        public EFRepositoryBase(IRepositoryContext context) 
            :base(context)
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

            //我在想，是否需要把删除、修改、插入放到上下文中提供抽象方法实现？
            //直接在仓储超类中使用下面这个方法是否可以呢？
            //其实RegisterNew内部调用的也是相同的方法。
            //这样设计不知道对于使用不同的持久化有没有好处

            //efContext.DbContext.Set<TAggregateRoot>().Add(aggregateRoot);
            //注意不要在这里提交。
            //需要在外面由efContext.DbContext.SaveChanges();
        }

        protected override bool DoExists(ISpecification<TAggregateRoot> specification)
        {
            //这种方式应该是先获取数据集合再COUNT
            //return efContext.DbContext.Set<TAggregateRoot>().Where(specification.GetExpression()).Count() > 0;

            //下面的应该是生成类 SELECT COUNT(1)的SQL语句，所以性能应该高一些
            //return efContext.DbContext.Set<TAggregateRoot>().Count(specification.GetExpression()) > 0;            
            return efContext.DbContext.Set<TAggregateRoot>().Count(specification.IsSatisfiedBy) > 0;
        }

        protected override TAggregateRoot DoGet(ISpecification<TAggregateRoot> wherePrecidate)
        {
            //Console.WriteLine(efContext.DbContext.Set<TAggregateRoot>().Where(wherePrecidate.IsSatisfiedBy).ToString());
            return efContext.DbContext.Set<TAggregateRoot>().Where(wherePrecidate.IsSatisfiedBy).FirstOrDefault();
        }

        protected override TAggregateRoot DoGet(Expression<Func<TAggregateRoot, bool>> wherePrecidate)
        {
            //Console.WriteLine(efContext.DbContext.Set<TAggregateRoot>().Where(wherePrecidate).ToString());
            return efContext.DbContext.Set<TAggregateRoot>().Where(wherePrecidate).FirstOrDefault();
        }

        protected override IQueryable<TAggregateRoot> DoGetALL()
        {
            return efContext.DbContext.Set<TAggregateRoot>();
        }

        protected override IQueryable<TAggregateRoot> DoGetALL(Expression<Func<TAggregateRoot, bool>> wherePrecidate)
        {
            return efContext.DbContext.Set<TAggregateRoot>().Where(wherePrecidate);

            //使用下面这个返回的是IQueryable<T>
            //efContext.DbContext.Set<TAggregateRoot>().Where(wherePrecidate);//
        }

        protected override IQueryable<TAggregateRoot> DoGetALL(Expression<Func<TAggregateRoot, bool>> wherePrecidate, Expression<Func<TAggregateRoot, dynamic>> orderPrecidate, SortOrder order)
        {
            var query = efContext.DbContext.Set<TAggregateRoot>().Where(wherePrecidate);
            if (null != orderPrecidate)
            {
                switch (order)
                {
                    case SortOrder.ASC:
                        return query.OrderBy(orderPrecidate);
                    case SortOrder.DESC:
                        return query.OrderByDescending(orderPrecidate);
                    default:
                        break;
                }
            }
            return query;
        }

        protected override PagedResult<TAggregateRoot> DoGetALL(Expression<Func<TAggregateRoot, bool>> wherePrecidate, Expression<Func<TAggregateRoot, dynamic>> orderPrecidate, SortOrder order, int iPageSize, int iPageIndex)
        {
            if(iPageSize <= 0)
                throw new ArgumentOutOfRangeException("iPageSize", iPageSize, "每页大小必须大于或等于1。");
            if (iPageIndex <= 0)
                throw new ArgumentOutOfRangeException("iPageIndex", iPageIndex, "当前页码必须大于或等于1。");

            int iSkip = (iPageIndex - 1) * iPageSize;
            int iTake = iPageSize;

            //注意：下面返回的是IQueryable<T>
            var query1 = this.DoGetALL(wherePrecidate, orderPrecidate, order);

            var iRecordCount = query1.Count();

            var iPageCount = iRecordCount % iPageSize == 0 ? iRecordCount / iPageSize : iRecordCount / iPageSize + 1;

            List<TAggregateRoot> dataList = query1.Skip(iSkip).Take(iTake).ToList();
            return new PagedResult<TAggregateRoot>(iRecordCount, iPageCount, iPageSize, iPageIndex, dataList);

            //var query = efContext.DbContext.Set<TAggregateRoot>().Where(wherePrecidate);

            //if (null != orderPrecidate)
            //{
            //    switch (order)
            //    {
            //        case SortOrder.ASC:
            //            var pagedGroupAscending = query.OrderBy(orderPrecidate).Skip(iSkip).Take(iTake).GroupBy(p => new
            //            {
            //                Total = query.Count()
            //            }).FirstOrDefault();
            //            if (pagedGroupAscending == null)
            //                return null;
            //            return new PagedResult<TAggregateRoot>(pagedGroupAscending.Key.Total, (pagedGroupAscending.Key.Total + iPageSize - 1) / iPageSize, iPageSize, iPageIndex, pagedGroupAscending.Select(p => p).ToList());
            //        case SortOrder.DESC:
            //            var pagedGroupDescending = query.OrderByDescending(orderPrecidate).Skip(iSkip).Take(iTake).GroupBy(p => new
            //            {
            //                Total = query.Count()
            //            }).FirstOrDefault();
            //            if (pagedGroupDescending == null)
            //                return null;
            //            return new PagedResult<TAggregateRoot>(pagedGroupDescending.Key.Total, (pagedGroupDescending.Key.Total + iPageSize - 1) / iPageSize, iPageSize, iPageIndex, pagedGroupDescending.Select(p => p).ToList());
                        
            //        default:
            //            break;
            //    }
            //}

            throw new InvalidOperationException("基于分页功能的查询必须指定排序字段和排序顺序。");
        }

        protected override IQueryable<TAggregateRoot> DoGetALL(ISpecification<TAggregateRoot> wherePrecidate)
        {
            return DoGetALL(wherePrecidate.GetExpression());
        }

        protected override IQueryable<TAggregateRoot> DoGetALL(ISpecification<TAggregateRoot> wherePrecidate, Expression<Func<TAggregateRoot, dynamic>> orderPrecidate, SortOrder order)
        {
            return DoGetALL(wherePrecidate.GetExpression(), orderPrecidate, order);
        }

        protected override PagedResult<TAggregateRoot> DoGetALL(ISpecification<TAggregateRoot> wherePrecidate, Expression<Func<TAggregateRoot, dynamic>> orderPrecidate, SortOrder order, int iPageSize, int iPageIndex)
        {
            return DoGetALL(wherePrecidate.GetExpression(), orderPrecidate, order, iPageSize, iPageIndex);
        }
       

        protected override TAggregateRoot DoGetByID(Guid id)
        {
            return efContext.DbContext.Set<TAggregateRoot>().Where(t => t.ID == id).First();
        }

        protected override void DoRemove(TAggregateRoot aggregateRoot)
        {
            efContext.RegisterDeleted<TAggregateRoot>(aggregateRoot);
        }

        protected override void DoUpdate(TAggregateRoot aggregateRoot)
        {
            efContext.RegisterModified<TAggregateRoot>(aggregateRoot);
        }

        protected override int DoGetCount()
        {
            return efContext.DbContext.Set<TAggregateRoot>().Count();
        }
    }
}
