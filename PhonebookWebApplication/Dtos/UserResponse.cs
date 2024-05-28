using PhonebookWebApplication.Models;
using System.ComponentModel.DataAnnotations;

namespace PhonebookWebApplication.Dtos
{
    public class UserResponse
    {
        public string? Message { get; set; }
    }

    public class DeleteResponse
    {
        public string? Message { get; set;}
        public int Id { get; set; }
    }

    public class UpdateUserResponse
    {
        public string? Message { get; set; }
        public int Id { get; set; }
    }

    public class GetUserDetailsResponse
    {
        public List<User> Users { get; set; }
        public string? Message { get; set; }
    }

    public class GetUserInfo
    {
        public User User { get; set; }
        public string? Message { get; set; }
    }
}
