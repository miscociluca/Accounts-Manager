using AccountManagementRedis.dto;
using AccountManagementRedis.Models;
using AccountManagementRedis.Services;
using AccountManagementRedis.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagementRedis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private  ICustomerService customerService;
        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            this.customerService = customerService;
        }

        /// <summary>
        /// Lists all existing customers
        /// </summary>
        /// 
        [HttpGet(Name = "Get All Customers")]
        public IEnumerable<Customer> GetAllCustomers()
        {
            var customerList = customerService.GetAllCustomers();
           return customerList;
        }

        /// <summary>
        /// Retrieves a specific customer by unique id
        /// </summary>
        /// <response code="404">Customer with id does not exists</response>
        /// 

        [HttpGet("{id}", Name = "Get Customer")]
        public ActionResult<Customer> getCustomerById(string id)
        {
            Customer customer = customerService.GetCustomerById(id);
            return customer != null ? customer : StatusCode(404, "Customer not found");
        }
    }
}
