using AccountManagementRedis.Models;

namespace AccountManagementRedis.Services.Interfaces
{
    public interface ITransactionService
    {
        Transaction createTransaction(decimal amount, Account account,Guid transactionId = new Guid());
    }
}
