using AccountManagementRedis.dto;
using AccountManagementRedis.Models;

namespace AccountManagementRedis.Services.Interfaces
{
    public interface ICustomerService
    {
        Customer GetCustomerById(string customerId);
        List<Customer> GetAllCustomers();
        Customer findCustomerById(string customerId);
    }
}
