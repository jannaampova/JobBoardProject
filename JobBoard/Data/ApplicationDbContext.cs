using JobBoard.Models; 
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Data
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<AccountType> AccountType { get; set; }
        public DbSet<Account> Account { get; set; }



    }
}
