using AccountManagement.Models;

namespace AccountManagement.dto
{
    public class TransactionDto
    {
        public Guid id { get; set; }

        public TransactionType? transactionType = TransactionType.INITIAL;
        public decimal? amount { get; set; }   
        public DateTime transactionDate {  get; set; }      

    }
}
