using AccountManagement.Services.Interfaces;
using System.Runtime.Caching;

namespace AccountManagement.Services
{
    public class CacheService : ICacheService
    {
        private MemoryCache _memoryCache = MemoryCache.Default;
        public T GetData<T>(string key)
        {
            try
            {
                T item = (T)_memoryCache.Get(key);
                return item;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void RemoveData(string key)
        {
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                     _memoryCache.Remove(key);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            bool res = true;
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    _memoryCache.Set(key, value, expirationTime);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return res;
        }
    }
}
