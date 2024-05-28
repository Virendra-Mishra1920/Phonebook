using System.ComponentModel.DataAnnotations;

namespace PhonebookWebApplication.Dtos
{
    public class UserRequest
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage ="Please enter valid email")]
        public string? Email { get; set; }
        [Required]

        [Phone(ErrorMessage ="Please enter valid phone number")]
        public string? Phone { get; set; }
    }


    public class UpdateUserRequest
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
