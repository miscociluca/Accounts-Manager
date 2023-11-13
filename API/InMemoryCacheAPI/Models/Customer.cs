namespace AccountManagement.Models
{
    public class Customer
    {
        public string? Id { get; set; } 

        public string? Name { get; set; }

        public string? SurName { get; set; }

        public List<Account>? Accounts { get; set; }

    }
}
