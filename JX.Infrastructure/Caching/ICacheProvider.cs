using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JX.Infrastructure.Caching
{
    /// <summary>
    /// 实现该接口的类型能够提供缓存机制
    /// </summary>
    public interface ICacheProvider
    {
        /// <summary>
        /// 缓存一个对象
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">需要缓存的对象</param>
        void Add(string key,object value);

        /// <summary>
        /// 更新缓存中的对象
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">需要缓存的对象</param>
        void Put(string key,object value);

        /// <summary>
        /// 从缓存中获取对象
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        object Get(string key);

        /// <summary>
        /// 从缓存中移除对象
        /// </summary>
        /// <param name="key"></param>
        void Delete(string key);

        /// <summary>
        /// 是否已经存在相应键的缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>如果缓存存在，则返回true，否则返回false。</returns>
        bool Exists(string key);

    }
}
