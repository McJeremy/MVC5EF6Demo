using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JX.Infrastructure.Caching
{
    public class CacheManager : ICacheProvider
    {
        private ICacheProvider _cacheProvider;
        private static readonly Lazy<CacheManager> _instance = new Lazy<CacheManager>();
        public static CacheManager Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private CacheManager()
        {
            _cacheProvider = ServiceLocator.Instance.GetService<ICacheProvider>();
        }

        public void Add(string key, object value)
        {
            _cacheProvider.Add(key, value);
        }

        public void Put(string key, object value)
        {
            _cacheProvider.Put(key, value);
        }

        public object Get(string key)
        {
            return _cacheProvider.Get(key);
        }

        public void Delete(string key)
        {
            _cacheProvider.Delete(key);
        }

        public bool Exists(string key)
        {
            return _cacheProvider.Exists(key);
        }
    }
}
