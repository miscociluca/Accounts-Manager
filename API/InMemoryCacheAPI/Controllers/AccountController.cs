using AccountManagement.dto;
using AccountManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private IAccountService accountService;
        public AccountController(ILogger<AccountController> logger, IAccountService accountService)
        {
            _logger = logger;
            this.accountService = accountService;
        }

        [HttpPost]
        public ObjectResult CreateAccount(CreateAccountRequest request)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError(string.Join("\r\n", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
                return  StatusCode(400, ModelState);
            }
            AccountDto newAccount = this.accountService.CreateAccount(request);
            return newAccount != null ? Ok(newAccount) : StatusCode(404, "Customer not found");
        }
    }
}
