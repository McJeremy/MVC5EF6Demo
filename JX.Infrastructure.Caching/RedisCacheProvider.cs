using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JX.Infrastructure.Caching
{
    public class RedisCacheProvider : ICacheProvider
    {
        public void Add(string key, object value)
        {
            throw new NotImplementedException();
        }

        public void Delete(string key)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string key)
        {
            throw new NotImplementedException();
        }

        public object Get(string key)
        {
            throw new NotImplementedException();
        }

        public void Put(string key, object value)
        {
            throw new NotImplementedException();
        }
    }
}
