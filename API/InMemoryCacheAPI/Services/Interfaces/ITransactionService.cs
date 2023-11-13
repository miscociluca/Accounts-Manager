using AccountManagement.Models;

namespace AccountManagement.Services.Interfaces
{
    public interface ITransactionService
    {
        Transaction createTransaction(decimal amount, Account account,Guid transactionId = new Guid());
    }
}
