using AutoMapper;
using AccountManagementRedis.dto;
using AccountManagementRedis.dto.interfaces;
using AccountManagementRedis.Helpers;
using AccountManagementRedis.Models;
using AccountManagementRedis.Services;
using AccountManagementRedis.Services.Interfaces;
using AccountManagementRedis.Repository;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;


namespace TestAccountManagement.service
{
    public  class AccountServiceTest
    {
        private Mock<IAccountRepository> _accountRepository;
        private AccountService _accountService;
        private Mock<ICustomerService> customerService;
        private TransactionService transactionService;
        private readonly MapperConfiguration _mapperConfiguration = new MapperConfiguration(cfg => {
            cfg.AddProfile(new MappingProfiles());
        });
        private readonly IMapper mapper;
        private Customer customer = new Customer { Id = "101", Name="testName", SurName="testSurname", Accounts = new List<Account>() };
        public AccountServiceTest()
        {
            mapper = _mapperConfiguration.CreateMapper();
            customerService = new Mock<ICustomerService>();
            transactionService = new TransactionService();   
        }


        [Fact]
        public void TestWhenCustomerIdExistsAndInitialCreditMoreThanZero_ShouldCreateAccountWithTransaction()
        {
            CreateAccountRequest accountRequest = new CreateAccountRequest{CustomerId = customer.Id,InitialCredit = 100};
            Account account = new Account(accountRequest.InitialCredit,DateTime.Now);
            Transaction transaction = new Transaction(accountRequest.InitialCredit,DateTime.Now,account);
            account.Transactions.Add(transaction);

            AccountDto expected = new AccountDto {
                Id = account.Id,
                Balance = account.Balance,
                CreationDate = account.CreationDate,
                Transactions = new List<TransactionDto> { mapper.Map<TransactionDto>(transaction) }   
            };
            _accountRepository = new Mock<IAccountRepository>();
            customerService.Setup(m => m.findCustomerById(customer.Id)).Returns(value: customer);

            _accountService = new AccountService(_accountRepository.Object,customerService.Object,transactionService,mapper);
            var accountResult = _accountService.CreateAccount(accountRequest,account.Id);
            Assert.NotNull(accountResult);
            Assert.Equal(accountResult.Id, expected.Id);
            Assert.Equal(accountResult.Transactions.Count, expected.Transactions.Count);
        }

        [Fact]
        public void TestWhenCustomerIdExistsAndInitialCreditIsZero_ShouldCreateAccountWithoutTransaction()
        {
            CreateAccountRequest accountRequest = new CreateAccountRequest { CustomerId = customer.Id, InitialCredit = 0 };
            Account account = new Account(accountRequest.InitialCredit, DateTime.Now);

            AccountDto expected = new AccountDto
            {
                Id = account.Id,
                Balance = account.Balance,
                CreationDate = account.CreationDate,
                Transactions = new List<TransactionDto>()
            };
            _accountRepository = new Mock<IAccountRepository>();
            _accountRepository.Setup(m => m.openNewAccount(customer.Id, account)).Returns(value: account);
            customerService.Setup(m => m.findCustomerById(customer.Id)).Returns(value: customer);

            _accountService = new AccountService(_accountRepository.Object, customerService.Object, transactionService, mapper);
            var accountResult = _accountService.CreateAccount(accountRequest, account.Id);
            Assert.NotNull(accountResult);
            Assert.Equal(accountResult.Id, expected.Id);
            Assert.Equal(accountResult.Transactions.Count, expected.Transactions.Count);
            Assert.Equal(accountResult.Transactions.Count,0);
        }


        [Fact]
        public void TestWhenCustomerIdDoesNotExists_ShouldReturnNull()
        {
            CreateAccountRequest accountRequest = new CreateAccountRequest { CustomerId = customer.Id, InitialCredit = 0 };
            Account account = new Account(accountRequest.InitialCredit, DateTime.Now);
            _accountRepository = new Mock<IAccountRepository>();
            _accountService = new AccountService(_accountRepository.Object, customerService.Object, transactionService, mapper);
            var accountResult = _accountService.CreateAccount(accountRequest, account.Id);
            Assert.Null(accountResult);
        }
    }
}
