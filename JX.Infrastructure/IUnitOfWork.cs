using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JX.Infrastructure
{
    public interface IUnitOfWork
    {
        // <summary>
        /// 获得一个<see cref="System.Boolean"/>值，该值表示当前的Unit Of Work是否支持Microsoft分布式事务处理机制。
        /// </summary>
        bool DistributedTransactionSupported
        {
            get;
        }

        /// <summary>
        /// 标识事务是否已经提交
        /// </summary>
        bool Committed
        {
            get;
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        void Commit();

        /// <summary>
        /// 回滚
        /// </summary>
        void Rollback();
    }
}
