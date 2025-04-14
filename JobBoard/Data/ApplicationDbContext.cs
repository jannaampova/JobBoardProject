using JobBoard.Models.plainModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobBoard.Data
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<AccountType> AccountType { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Town> Town { get; set; }
        public DbSet<Industry> Industry { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<JobType> JobType { get; set; }
        public DbSet<Benefits> Benefits { get; set; }
        public DbSet<Requirements> Requirements { get; set; }
        public DbSet<Listing> Listings { get; set; }
        [NotMapped]
        public DbSet<ListingRequirements> ListingRequirements { get; set; }
        [NotMapped]
        public DbSet<ListingBenefits> ListingBenefits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ListingRequirements>().HasNoKey();
            modelBuilder.Entity<ListingBenefits>().HasNoKey();
            modelBuilder.Entity<CandidateSkills>().HasNoKey();
            modelBuilder.Entity<AccountType>().HasKey(a => a.Id);

        }

        public DbSet<Skills> Skills { get; set; }
        public DbSet<Candidate> Candidate { get; set; }
        public DbSet<CandidateSkills> CandidateSkills { get; set; }
        public DbSet<Application> Application { get; set; }

        







    }
}
