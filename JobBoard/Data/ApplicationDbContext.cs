﻿using JobBoard.Models.plainModels;
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
        
        public DbSet<Skills> Skills { get; set; }
        public DbSet<Candidate> Candidate { get; set; }
        public DbSet<CandidateSkills> CandidateSkills { get; set; }
        public DbSet<Application> Application { get; set; }
        public DbSet<SavedListings> SavedListings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ListingRequirements>()
                .HasKey(lr => new { lr.ListingId, lr.RequirementId });;
            modelBuilder.Entity<ListingBenefits>()
                .HasKey(lb => new { lb.ListingId, lb.BenefitId });;
            modelBuilder.Entity<CandidateSkills>()
                .HasKey(cs => new { cs.CandidateId, cs.SkillId });           
            modelBuilder.Entity<AccountType>().HasKey(a => a.Id);

            modelBuilder.Entity<SavedListings>()
                .HasKey(sl => new { sl.CandidateId, sl.ListingId });

            modelBuilder.Entity<SavedListings>()
                .HasOne(sl => sl.candidate)
                .WithMany()
                .HasForeignKey(sl => sl.CandidateId)
                .OnDelete(DeleteBehavior.Cascade); // keep cascade here

            modelBuilder.Entity<SavedListings>()
                .HasOne(sl => sl.listing)
                .WithMany()
                .HasForeignKey(sl => sl.ListingId)
                .OnDelete(DeleteBehavior.Restrict); // prevent cycle here
        }
        


        







    }
}
