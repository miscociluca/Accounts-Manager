
using AccountManagementRedis.Controllers;
using AccountManagementRedis.dto.interfaces;
using AccountManagementRedis.Helpers;
using AccountManagementRedis.Models;
using AccountManagementRedis.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace TestAccountManagement.controller
{
    public class CustomerControllerTest
    {
        private readonly Mock<ILogger<CustomerController>> _logger;
        private Mock<ICustomerRepository> _customerRepository;
        private CustomerService _customerService;
        private CustomerController _controller;
        private readonly MapperConfiguration _mapperConfiguration = new MapperConfiguration(cfg => {
            cfg.AddProfile(new MappingProfiles());
        });
        private readonly List<Customer> customers = new List<Customer>();
        private readonly IMapper mapper;
        public CustomerControllerTest()
        {
            _logger = new Mock<ILogger<CustomerController>>();
            mapper = _mapperConfiguration.CreateMapper();
            customers = GetCustomersInitialData();
        }

        [Fact]
        public void GetCustomerById_Returns_ActionResult()
        {
            _customerRepository = new Mock<ICustomerRepository>();
            _customerService = new CustomerService(_customerRepository.Object, mapper);
            _controller = new CustomerController(_logger.Object, _customerService);
            var result = _controller.getCustomerById("1");
            Assert.IsType<ActionResult<Customer>>(result);
        }

        [Fact]
        public void TestGetCustomerList_CustomerList_shouldReturnCustomersList()
        {
            _customerRepository = new Mock<ICustomerRepository>();
            _customerRepository.Setup(m => m.findAll()).Returns(value: customers);
            _customerService = new CustomerService(_customerRepository.Object, mapper);
            _controller = new CustomerController(_logger.Object, _customerService);
            var customersResult =_controller.GetAllCustomers().ToList();
            Assert.NotNull(customersResult);
            Assert.Equal(customers.Count, customersResult.Count);
            Assert.Equal(customers,customersResult);
        }

        [Fact]
        public void TestGetCustomerByID_whenCustomerIdExists_shouldReturnCustomer()
        {
            var customer = customers.Find(x => x.Id == "3");
            _customerRepository = new Mock<ICustomerRepository>();
            _customerRepository.Setup(m => m.findById("3")).Returns(value: customer);
            _customerService = new CustomerService(_customerRepository.Object, mapper);
            _controller = new CustomerController(_logger.Object, _customerService);

            var customerResult = _controller.getCustomerById("3");
            Assert.NotNull(customerResult);
            Assert.Equal(customer.Id, customerResult.Value.Id);
            Assert.True(customer.SurName == customerResult.Value.SurName);
        }

        [Fact]
        public void TestGetCustomerByID_whenCustomerDoesNotExist_shouldReturnHttpNotFound()
        {
            var customer = customers.Find(x => x.Id == "100");
            _customerRepository = new Mock<ICustomerRepository>();
            _customerRepository.Setup(m => m.findById("100")).Returns(value: customer);
            _customerService = new CustomerService(_customerRepository.Object, mapper);
            _controller = new CustomerController(_logger.Object, _customerService);

            var customerResult = _controller.getCustomerById("100");
            var statuscode = GetStatusCode(customerResult);
            Assert.Equal(404, statuscode);
        }

        private List<Customer>? GetCustomersInitialData()
        {
            using (StreamReader r = new StreamReader(Directory.GetCurrentDirectory() + @"\customers.json"))
            {
                string json = r.ReadToEnd();
                List<Customer>? customers = JsonConvert.DeserializeObject<List<Customer>>(json);
                if (customers != null)
                {
                    return customers;
                }
            }
            return null;
        }

        private static int? GetStatusCode<T>(ActionResult<T?> actionResult)
        {
            IConvertToActionResult convertToActionResult = actionResult;
            var actionResultWithStatusCode = convertToActionResult.Convert() as IStatusCodeActionResult;
            return actionResultWithStatusCode?.StatusCode;
        }
    }
}
