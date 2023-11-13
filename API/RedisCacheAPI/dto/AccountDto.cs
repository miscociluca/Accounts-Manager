using System.Collections.Generic;

namespace AccountManagementRedis.dto
{
    public class AccountDto
    {
        public Guid Id { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreationDate { get; set; }
        public List<TransactionDto>? Transactions { get; set; }
    }
}
