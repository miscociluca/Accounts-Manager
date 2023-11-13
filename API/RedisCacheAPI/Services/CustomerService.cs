using AutoMapper;
using AccountManagementRedis.dto.interfaces;
using AccountManagementRedis.Models;
using AccountManagementRedis.Services.Interfaces;

namespace AccountManagementRedis.Services
{
    public class CustomerService: ICustomerService
    {
        private  ICustomerRepository customerRepository;
        public readonly IMapper _mapper;
        public CustomerService(ICustomerRepository customerRepository, IMapper mapper) { 
            this.customerRepository = customerRepository;
            this._mapper = mapper;
        }

        public Customer GetCustomerById(string customerId)
        {
            var customer = customerRepository.findById(customerId);
            return customer;  
        }

        public List<Customer> GetAllCustomers()
        {
            var _mappedCustomers = customerRepository.findAll();
            return _mappedCustomers;
        }

        public Customer findCustomerById(string customerId)
        {
            var customer = customerRepository.findById(customerId); 
            return customer;
        }
    }
}
