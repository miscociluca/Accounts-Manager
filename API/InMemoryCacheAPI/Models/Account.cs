namespace AccountManagement.Models
{
    public class Account
    {

        public Account(decimal initialCredit, DateTime now,Guid ID = new Guid())
        {
            Id = ID!= Guid.Empty?ID : Guid.NewGuid();
            this.Balance = initialCredit;
            this.CreationDate = now;
        }

        public Guid Id { get; set; }
        public decimal Balance { get; set; } =  0;  

        public DateTime CreationDate { get; set; }   

        public List<Transaction> Transactions { get; set; } = new List<Transaction>();  


    }

    public enum TransactionType
    {
        INITIAL, TRANSFER
    }
}
