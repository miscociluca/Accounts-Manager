using AccountManagement.dto.interfaces;
using AccountManagement.Models;
using AccountManagement.Services.Interfaces;
using Blue_Harvest_Redis.Constants;

namespace AccountManagement.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ICacheService _cacheService;
        public CustomerRepository(ICacheService cacheService) {
            _cacheService = cacheService;
        }
        public int addCustomer(Customer customerEntity)
        {
            throw new NotImplementedException();
        }
        public List<Customer> findAll()
        {
            var exitingCacheData = _cacheService.GetData<IEnumerable<Customer>>(CacheKeys.CUSTOMERS_KEY);
            return exitingCacheData!=null? exitingCacheData.ToList() : new List<Customer>();

        }
        public Customer? findById(string id)
        {
            var exitingCacheData = _cacheService.GetData<IEnumerable<Customer>>(CacheKeys.CUSTOMERS_KEY);
            if (exitingCacheData != null)
            {
                var exisitingCustomer = exitingCacheData.ToList().Find(x => x.Id == id);
                if (exisitingCustomer != null)
                {
                    return exisitingCustomer;
                }
            }
            return null;
        }

        public Customer Save(Customer customer)
        {
            var exitingCacheData = _cacheService.GetData<IEnumerable<Customer>>(CacheKeys.CUSTOMERS_KEY);
            Customer newCustomer = new Customer();
            if (exitingCacheData != null)
            {
                var exisitingCustomer = exitingCacheData.ToList().Find(x => x.Id == customer.Id);
                if (exisitingCustomer != null)
                {
                    exisitingCustomer.SurName = customer.SurName;
                    exisitingCustomer.Name = customer.Name;
                    exisitingCustomer.Accounts = customer.Accounts;
                    newCustomer = exisitingCustomer;    
                }
                else
                {
                    exitingCacheData.ToList().Add(customer);
                    newCustomer = customer;
                }
                try
                {
                    _cacheService.SetData(CacheKeys.CUSTOMERS_KEY, exitingCacheData, new DateTimeOffset(DateTime.Now.AddHours(1)));
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return newCustomer;
        }

    }
}
