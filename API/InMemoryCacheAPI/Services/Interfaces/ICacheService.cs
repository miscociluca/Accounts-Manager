using AccountManagement.Models;
using System;
using System.Runtime.Caching;

namespace AccountManagement.Services.Interfaces
{
    public interface ICacheService
    {
        T GetData<T>(string key);
        bool SetData<T>(string key, T value, DateTimeOffset expirationTime);
        void RemoveData(string key);
    }
}
