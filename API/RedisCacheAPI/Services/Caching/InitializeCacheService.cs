using AccountManagementRedis.Models;
using AccountManagementRedis.Services;
using AccountManagementRedis.Services.Interfaces;
using AccountManagementRedis.Constants;
using Newtonsoft.Json;

namespace AccountManagementRedis.Services.Caching
{
    public class InitializeCacheService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        public InitializeCacheService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            //Insert some initial dummy data in REDIS
            using (var scope = _serviceProvider.CreateScope())
            {
                var _cacheService = _serviceProvider.GetService<ICacheService>();
                if (_cacheService != null)
                {
                    using (StreamReader r = new StreamReader(Directory.GetCurrentDirectory() + @"/customers.json"))
                    {
                        string json = r.ReadToEnd();
                        List<Customer>? customers = JsonConvert.DeserializeObject<List<Customer>>(json);
                        if (customers != null)
                        {
                            _cacheService.SetCachedData(CacheKeys.CUSTOMERS_KEY, customers, TimeSpan.FromHours(1));
                        }
                    }
                }
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
