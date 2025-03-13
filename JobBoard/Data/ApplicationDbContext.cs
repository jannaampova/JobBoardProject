using JobBoard.Models; 
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Data
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<AccountType> AccountType { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<Candidate> Candidate { get; set; }
        public DbSet<CandidateSkills> CandidateSkills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CandidateSkills>().HasNoKey();
        }






    }
}
