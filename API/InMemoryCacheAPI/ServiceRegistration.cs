using AccountManagement.dto.interfaces;
using AccountManagement.Repository;
using AccountManagement.Services.Interfaces;
using AccountManagement.Services;
using Blue_Harvest_Redis.Services;

namespace AccountManagement
{
    public static  class ServiceRegistration
    {
        public static void AddDataInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region 
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();

            services.AddSingleton<ICacheService, CacheService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ITransactionService, TransactionService>();
            #endregion
        }
    }
}
