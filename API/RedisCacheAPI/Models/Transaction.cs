using AccountManagementRedis.dto;
using System.Security.Principal;
using System.Text.Json.Serialization;

namespace AccountManagementRedis.Models
{
    public class Transaction
    {
        public Transaction(decimal amount, DateTime transactionDate, Account account ,
            TransactionType transactionType = TransactionType.INITIAL, Guid ID = new Guid())
        {
            Id = ID != Guid.Empty ? ID : Guid.NewGuid();
            Amount = amount;
            TransactionDate = transactionDate;
            TransactionType = transactionType;
        }

        public Guid Id { get; set; }

        public TransactionType TransactionType = TransactionType.INITIAL;
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
