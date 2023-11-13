using AccountManagement.Models;

namespace AccountManagement.dto.interfaces
{
    public interface IAccountRepository
    {
        Account openNewAccount(string customerId,Account accountEntity);
    }
}
