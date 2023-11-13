using AccountManagement.dto;
using System.Security.Principal;

namespace AccountManagement.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }

        public TransactionType TransactionType = TransactionType.INITIAL;
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
       
        public Transaction(decimal amount, DateTime transactionDate,
            TransactionType transactionType = TransactionType.INITIAL, Guid ID = new Guid())
        {
            Id = ID != Guid.Empty ? ID : Guid.NewGuid();
            Amount = amount;
            TransactionDate = transactionDate;
            TransactionType = transactionType;
        }
    }
}
