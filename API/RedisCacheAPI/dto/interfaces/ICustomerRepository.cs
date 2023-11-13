using AccountManagementRedis.Models;

namespace AccountManagementRedis.dto.interfaces
{
    public interface ICustomerRepository
    {
        int addCustomer(Customer customerEntity);

        Customer? findById(string id);  

        List<Customer> findAll();
    }
}
