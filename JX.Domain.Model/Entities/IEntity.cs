using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JX.Domain.Model.Entities
{
    /*
    所有的实体都要实现这个接口。
    注意，这个实体接口和仓储模式的IRepository接口不是一回事，也和DDD中的聚合不是一回事。但他们之间可能会有关联。
    */
    /// <summary>
    /// 实体接口
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// 唯一标识符
        /// </summary>
        Guid ID
        { get;set; }
    }
}
