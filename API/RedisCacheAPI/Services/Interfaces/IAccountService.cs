using AccountManagementRedis.dto;

namespace AccountManagementRedis.Services.Interfaces
{
    public interface IAccountService
    {
        AccountDto CreateAccount(CreateAccountRequest createAccountRequest);

    }
}
