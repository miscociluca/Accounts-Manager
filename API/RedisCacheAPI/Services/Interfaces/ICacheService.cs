using AccountManagementRedis.Models;
using System;
using System.Runtime.Caching;

namespace AccountManagementRedis.Services.Interfaces
{
    public interface ICacheService
    {
        T GetData<T>(string key);
        void SetCachedData<T>(string key, T data, TimeSpan cacheDuration);
        void RemoveData(string key);
    }
}
