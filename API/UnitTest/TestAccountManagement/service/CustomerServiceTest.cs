using AutoMapper;
using AccountManagementRedis.dto.interfaces;
using AccountManagementRedis.Helpers;
using AccountManagementRedis.Models;
using AccountManagementRedis.Services;
using Moq;
using Xunit;

namespace TestAccountManagement.service
{
    public class CustomerServiceTest
    {
        private Mock<ICustomerRepository> _customerRepository;
        private CustomerService _customerService;
        private readonly MapperConfiguration _mapperConfiguration = new MapperConfiguration(cfg => {
            cfg.AddProfile(new MappingProfiles());
        });
        private readonly IMapper mapper;
        public CustomerServiceTest() {
            mapper = _mapperConfiguration.CreateMapper();
        }

        [Fact]
        public void TestFindByCustomerId_whenCustomerIdExists_shouldReturnCustomer()
        {
            var customer = new Customer { 
            Id= "101",
            SurName="dave",
            Name="splane"
            };
            _customerRepository = new Mock<ICustomerRepository>();
            _customerRepository.Setup(m => m.findById("101")).Returns(value: customer);
            _customerService = new CustomerService(_customerRepository.Object, mapper);
         
            var customerResult = _customerService.findCustomerById("101");
            Assert.NotNull(customerResult);
            Assert.Equal(customerResult, customer);
        }

        [Fact]
        public void TestFindByCustomerId_whenCustomerDoesNotExist_shouldThrowCustomerNotFoundException()
        {
            Customer customer = new Customer();
            _customerRepository = new Mock<ICustomerRepository>();
            _customerRepository.Setup(m => m.findById("101")).Returns(value: null);
            _customerService = new CustomerService(_customerRepository.Object, mapper);
            var customerResult = _customerService.findCustomerById("101");
            Assert.Null(customerResult);
        }
    }
}
