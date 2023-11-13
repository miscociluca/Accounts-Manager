using AutoMapper;
using AccountManagementRedis.dto;
using AccountManagementRedis.dto.interfaces;
using AccountManagementRedis.Models;
using AccountManagementRedis.Services.Interfaces;

namespace AccountManagementRedis.Services
{
    public class AccountService: IAccountService
    {
        private  IAccountRepository accountRepository;
        private ICustomerService customerService;
        private ITransactionService transactionService;
        public readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepository,
                          ICustomerService customerService, ITransactionService transactionService, IMapper mapper)
        {
            this.accountRepository = accountRepository;
            this.customerService = customerService;
            this.transactionService = transactionService;   
            this._mapper = mapper;
        }


        public AccountDto? CreateAccount(CreateAccountRequest createAccountRequest)
        {
            Customer customer = customerService.findCustomerById(createAccountRequest.CustomerId);
            AccountDto accountDto;
            if (customer != null)
            {
                Account account = new(
                        createAccountRequest.InitialCredit,
                        DateTime.Now);

                if (createAccountRequest.InitialCredit > 0)
                {
                    Transaction transaction = transactionService.createTransaction(createAccountRequest.InitialCredit, account);
                    account.Transactions.Add(transaction);
                }
                accountDto = _mapper.Map<AccountDto>(accountRepository.openNewAccount(customer.Id, account));
                return accountDto;
            }
            return null;
        }


        public AccountDto? CreateAccount(CreateAccountRequest createAccountRequest, Guid accountId = new Guid())
        {
            Customer customer = customerService.findCustomerById(createAccountRequest.CustomerId);
            AccountDto accountDto;
            if (customer != null)
            {
                Account account = new(
                        createAccountRequest.InitialCredit,
                        DateTime.Now,
                        accountId);

                if (createAccountRequest.InitialCredit > 0)
                {
                    Transaction transaction = transactionService.createTransaction(createAccountRequest.InitialCredit, account);
                    account.Transactions.Add(transaction);
                }
                accountDto = _mapper.Map<AccountDto>(account);
                return accountDto;
            }
            return null;
        }
    }
}
