using JobBoard.Models; 
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Data
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Job> Jobs { get; set; } 
    }
}
