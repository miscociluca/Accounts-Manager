using AccountManagementRedis.dto.interfaces;
using AccountManagementRedis.Repository;
using AccountManagementRedis.Services.Interfaces;
using AccountManagementRedis.Services;
using AccountManagementRedis.Services.Caching;

namespace AccountManagementRedis
{
    public static  class ServiceRegistration
    {
        public static void AddDataInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region 
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();

            services.AddSingleton<ICacheService, RedisCacheService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ITransactionService, TransactionService>();
            #endregion
        }
    }
}
