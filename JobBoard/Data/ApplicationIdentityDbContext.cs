using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using JobBoard.Security;
using Microsoft.EntityFrameworkCore;
using JobBoard.Models;

namespace JobBoard.Data
{
    public class ApplicationIdentityDbContext : IdentityDbContext<UserData>
    {
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options) : 
            base(options)
        {
        }

        public DbSet<AccountType> AccountTypes { get; set; }

    }
}
