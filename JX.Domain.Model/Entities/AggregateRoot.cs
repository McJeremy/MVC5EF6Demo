using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JX.Domain.Model.Entities
{
    /// <summary>
    /// 表示聚合根类型的基类型 
    /// </summary>
    public class AggregateRoot :IAggregateRoot
    {
        public Guid ID
        { get;set; }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }

        /// <summary>
        /// 确定指定的Object是否等于当前的Object。
        /// </summary>
        /// <param name="obj">要与当前对象进行比较的对象。</param>
        /// <returns>如果指定的Object与当前Object相等，则返回true，否则返回false。</returns>
        /// <remarks>有关此函数的更多信息，请参见：http://msdn.microsoft.com/zh-cn/library/system.object.equals。
        /// </remarks>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            IAggregateRoot ar = obj as IAggregateRoot;
            if (null == ar)
            {
                return false;
            }

            return this.ID == ar.ID;
        }
    }
}
