using AccountManagementRedis.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace AccountManagementRedis.Services.Caching
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public T GetData<T>(string key)
        {
            var jsonData = _cache.GetString(key);

            if (jsonData == null)
                return default;

            return JsonConvert.DeserializeObject<T>(jsonData);
        }

        public void SetCachedData<T>(string key, T data, TimeSpan cacheDuration)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = cacheDuration,
                SlidingExpiration = cacheDuration
            };

            var jsonData = JsonConvert.SerializeObject(data);
            _cache.SetString(key, jsonData, options);
        }
        public void RemoveData(string key)
        {
            _cache.Remove(key);
        }
    }
}
