using Microsoft.EntityFrameworkCore;
using PhonebookWebApplication.Models;

namespace PhonebookWebApplication.Data
{
    public class UserDbContext:DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options):base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
    }
}
