namespace AccountManagementRedis.dto
{
    public class CustomerDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public List<AccountDto>? Accounts { get; set; }    
    }
}
