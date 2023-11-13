using AccountManagement.dto;
using AccountManagement.Models;

namespace AccountManagement.Services.Interfaces
{
    public interface ICustomerService
    {
        Customer GetCustomerById(string customerId);
        List<Customer> GetAllCustomers();
        Customer findCustomerById(string customerId);
    }
}
