using AccountManagementRedis.dto.interfaces;
using AccountManagementRedis.Models;
using AccountManagementRedis.Services.Interfaces;
using AccountManagementRedis.Constants;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace AccountManagementRedis.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ICacheService _cacheService;
        public AccountRepository(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        public Account openNewAccount(string customerId, Account accountEntity)
        {
            var exitingCacheData = _cacheService.GetData<IEnumerable<Customer>>(CacheKeys.CUSTOMERS_KEY);
            if (exitingCacheData != null)
            {
                var exisitingCustomer = exitingCacheData.ToList().Find(x => x.Id == customerId);
                if (exisitingCustomer != null)
                {
                    if (exisitingCustomer.Accounts == null) { exisitingCustomer.Accounts = new List<Account>(); }
                    exisitingCustomer.Accounts.Add(accountEntity);

                    try
                    {
                        _cacheService.SetCachedData(CacheKeys.CUSTOMERS_KEY, exitingCacheData, TimeSpan.FromHours(1));
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    return accountEntity;
                }
            }
            return null;
        }
    }
}
