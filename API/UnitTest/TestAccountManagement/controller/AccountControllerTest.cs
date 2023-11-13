
using AccountManagementRedis.Controllers;
using AccountManagementRedis.dto;
using AccountManagementRedis.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace TestAccountManagement.controller
{
    public class AccountControllerTest
    {
        private readonly Mock<ILogger<AccountController>> _logger;
        private Mock<IAccountService> _accountService;
        private readonly AccountController _controller;
        private CreateAccountRequest? request = null;
        public AccountControllerTest()
        {
            _accountService = new Mock<IAccountService>();
            _logger = new Mock<ILogger<AccountController>>();
            _controller = new AccountController(_logger.Object, _accountService.Object);
        }

        [Fact]
        public void CreateAccount_ObjectResult_Returns()
        {
            request = new CreateAccountRequest();
            request.CustomerId = "1";
            request.InitialCredit = 0;
            var result = _controller.CreateAccount(request);
            Assert.IsType<ObjectResult>(result);
        }

        [Fact]
        public void Create_InvalidModel_NoCustomer_CreateAccountNeverExecutes()
        {
            _controller.ModelState.AddModelError("CustomerId", "CustomerId is required.");
            request = new CreateAccountRequest { InitialCredit=100 };
            _controller.CreateAccount(request);
            _accountService.Verify(x => x.CreateAccount(It.IsAny<CreateAccountRequest>()), Times.Never);
        }

        [Fact]
        public void Create_InvalidModel_LessThanZeroInitialCreditValue_CreateAccountNeverExecutes()
        {
            _controller.ModelState.AddModelError("InitialCredit", "Initial Credit value must NOT BE negative value");
            request = new CreateAccountRequest { InitialCredit = -10 };
            _controller.CreateAccount(request);
            _accountService.Verify(x => x.CreateAccount(It.IsAny<CreateAccountRequest>()), Times.Never);
        }

        [Fact]
        public void Create_ModelStateValid_CreateAccountCalledOnce()
        {
            CreateAccountRequest? accountRequest = null;
            _accountService.Setup(r => r.CreateAccount(It.IsAny<CreateAccountRequest>()))
                .Callback<CreateAccountRequest>(x => accountRequest = x);
            var account = new CreateAccountRequest
            {
                InitialCredit = 100,
                CustomerId = "1",
            };
            var result = _controller.CreateAccount(account);
            _accountService.Verify(x => x.CreateAccount(It.IsAny<CreateAccountRequest>()), Times.Once);
            Assert.Equal(accountRequest.InitialCredit, account.InitialCredit);
            Assert.Equal(accountRequest.CustomerId, account.CustomerId);
        }

        [Fact]
        public void CreateAccountWithNotFoundCustomer_Returns400BadRequest()
        {
            var accountRequest = new CreateAccountRequest
            {
                CustomerId = "100",
                InitialCredit=11
            };
            var result = _controller.CreateAccount(accountRequest);
            _accountService.Verify(x => x.CreateAccount(It.IsAny<CreateAccountRequest>()), Times.Once);

            Assert.Equal(404, result.StatusCode);
        }

    }
}
