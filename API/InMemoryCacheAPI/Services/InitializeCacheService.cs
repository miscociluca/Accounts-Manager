using AccountManagement.Models;
using AccountManagement.Services.Interfaces;
using Blue_Harvest_Redis.Constants;
using Newtonsoft.Json;

namespace Blue_Harvest_Redis.Services
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
            //Insert some initial dummy data
            using (var scope = _serviceProvider.CreateScope())
            {
                var _cacheService = _serviceProvider.GetService<ICacheService>();
                if (_cacheService != null)
                {
                    using (StreamReader r = new StreamReader(Directory.GetCurrentDirectory() + @"\Constants\customers.json"))
                    {
                        string json = r.ReadToEnd();
                        List<Customer>? customers = JsonConvert.DeserializeObject<List<Customer>>(json);
                        if (customers != null)
                        {
                            _cacheService.SetData(CacheKeys.CUSTOMERS_KEY, customers, new DateTimeOffset(DateTime.Now.AddHours(1)));
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
