using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JX.Infrastructure
{
    /// <summary>
    ///  表示包含了分页信息的集合类型。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResult<T> :IEnumerable<T>,ICollection<T>
    {
        // <summary>
        /// 获取或设置总记录数。
        /// </summary>
        public int TotalRecords
        {
            get; set;
        }
        /// <summary>
        /// 获取或设置页数。
        /// </summary>
        public int TotalPages
        {
            get; set;
        }
        /// <summary>
        /// 获取或设置页面大小。
        /// </summary>
        public int PageSize
        {
            get; set;
        }
        /// <summary>
        /// 获取或设置页码。
        /// </summary>
        public int PageNumber
        {
            get; set;
        }
        /// <summary>
        /// 获取或设置当前页面的数据。
        /// </summary>
        public List<T> Data
        {
            get; set;
        }

        public int Count
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsReadOnly
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public static readonly PagedResult<T> Empty = new PagedResult<T>(0, 0, 0, 0, null);

        public PagedResult()
        {
            this.Data = new List<T>();
        }

        public PagedResult(int iTotalRecords, int iTotalPages, int iPageSize, int iPageNumber, List<T> dat)
        {
            this.TotalPages = iTotalPages;
            this.TotalRecords = iTotalRecords;
            this.PageSize = iPageSize;
            this.PageNumber = iPageNumber;

            if (null == dat)
            {
                this.Data = new List<T>();
            }
            else
            {
                this.Data = dat;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        public void Add(T item)
        {
            this.Data.Add(item);
        }

        public void Clear()
        {
            this.Data.Clear();
        }

        public bool Contains(T item)
        {
            return this.Data.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.Data.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return this.Data.Remove(item);
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
            if (ReferenceEquals(this, obj))
                return true;
            if (obj == (object)null)
                return false;
            var other = obj as PagedResult<T>;
            if (other == (object)null)
                return false;
            return this.TotalPages == other.TotalPages &&
                this.TotalRecords == other.TotalRecords &&
                this.PageNumber == other.PageNumber &&
                this.PageSize == other.PageSize &&
                this.Data == other.Data;
        }

        /// <summary>
        /// 用作特定类型的哈希函数。
        /// </summary>
        /// <returns>当前Object的哈希代码。</returns>
        /// <remarks>有关此函数的更多信息，请参见：http://msdn.microsoft.com/zh-cn/library/system.object.gethashcode。
        /// </remarks>
        public override int GetHashCode()
        {
            return this.TotalPages.GetHashCode() ^
                this.TotalRecords.GetHashCode() ^
                this.PageNumber.GetHashCode() ^
                this.PageSize.GetHashCode();
        }

        /// <summary>
        /// 确定两个对象是否相等。
        /// </summary>
        /// <param name="a">待确定的第一个对象。</param>
        /// <param name="b">待确定的另一个对象。</param>
        /// <returns>如果两者相等，则返回true，否则返回false。</returns>
        public static bool operator ==(PagedResult<T> a, PagedResult<T> b)
        {
            if (ReferenceEquals(a, b))
                return true;
            if ((object)a == null || (object)b == null)
                return false;
            return a.Equals(b);
        }

        /// <summary>
        /// 确定两个对象是否不相等。
        /// </summary>
        /// <param name="a">待确定的第一个对象。</param>
        /// <param name="b">待确定的另一个对象。</param>
        /// <returns>如果两者不相等，则返回true，否则返回false。</returns>
        public static bool operator !=(PagedResult<T> a, PagedResult<T> b)
        {
            return !(a == b);
        }
    }
}
