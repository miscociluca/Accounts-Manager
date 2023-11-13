using System.ComponentModel.DataAnnotations;

namespace AccountManagement.dto
{
    public class CreateAccountRequest
    {
        [Required(ErrorMessage = "CustomerId is required.")]
        [MinLength(1,ErrorMessage = "CustomerId must not be empty")]  
        public string CustomerId { get; set; }

        [Required(ErrorMessage = "Initial Credit is required.")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "Initial Credit value must NOT BE negative value")]
        public decimal InitialCredit { get; set; }
    }
}
