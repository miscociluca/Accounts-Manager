using AccountManagementRedis.Models;

namespace AccountManagementRedis.dto.interfaces
{
    public interface IAccountRepository
    {
        Account openNewAccount(string customerId,Account accountEntity);
    }
}
