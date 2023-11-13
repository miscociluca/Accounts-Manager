using AccountManagement.dto.interfaces;
using AccountManagement.Models;
using AccountManagement.Services.Interfaces;
using Blue_Harvest_Redis.Constants;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace AccountManagement.Repository
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
                        _cacheService.SetData(CacheKeys.CUSTOMERS_KEY, exitingCacheData, new DateTimeOffset(DateTime.Now.AddHours(1)));
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
