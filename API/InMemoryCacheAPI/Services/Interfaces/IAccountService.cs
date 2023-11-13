using AccountManagement.dto;

namespace AccountManagement.Services.Interfaces
{
    public interface IAccountService
    {
        AccountDto CreateAccount(CreateAccountRequest createAccountRequest);

    }
}
