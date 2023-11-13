using AccountManagementRedis.dto;
using AccountManagementRedis.Models;
using AccountManagementRedis.Services.Interfaces;

namespace AccountManagementRedis.Services
{
    public class TransactionService:ITransactionService
    {
        public TransactionService() { }

        public Transaction createTransaction(decimal amount, Account account, Guid transactionId = new Guid())
        {

            Transaction transaction = new Transaction(
                        amount,
                        DateTime.Now,
                        account,TransactionType.INITIAL, transactionId);
            return transaction; 
        }
    }
}
