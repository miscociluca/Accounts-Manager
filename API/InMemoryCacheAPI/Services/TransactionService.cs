using AccountManagement.Models;
using AccountManagement.Services.Interfaces;

namespace AccountManagement.Services
{
    public class TransactionService:ITransactionService
    {
        public TransactionService() { }

        public Transaction createTransaction(decimal amount, Account account, Guid transactionId = new Guid())
        {

            Transaction transaction = new Transaction(
                        amount,
                        DateTime.Now,TransactionType.INITIAL, transactionId);
            return transaction; 
        }
    }
}
