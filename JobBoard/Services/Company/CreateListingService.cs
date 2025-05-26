﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobBoard.Data;
using JobBoard.Models.plainModels;
using JobBoard.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Services.Company
{
    public class CreateListingService
    {
        private readonly ApplicationDbContext _context;
        public CreateListingService(ApplicationDbContext context)
        {
            _context = context;
        }

 
        public async Task<int> CreateListingAsync(CreateListingViewModel model)
        {
            
            var listing = new Listing
            {
                companyId        = model.CompanyId,
                title            = model.Title,
                Description      = model.Description,
                datePosted       = model.DatePosted,
                jobTypeId        = model.JobTypeId,
                townId           = model.TownId,
                experienceLevel  = model.Level,
                status="Active"
  
            };


            _context.Listings.Add(listing);
            await _context.SaveChangesAsync();
          
                listing.listingRequirements = new List<ListingRequirements>();
                listing.listingBenefits = new List<ListingBenefits>();
            


            foreach (var reqId in model.RequirementIds)
            {
                var listingRequirement=new ListingRequirements
                {
                    ListingId     = listing.Id,
                    RequirementId = reqId
                };
                listing.listingRequirements.Add(listingRequirement);
                _context.ListingRequirements.Add(listingRequirement);
            }

            foreach (var benId in model.BenefitIds)
            {
                var listingBenefit = new ListingBenefits
                {
                    ListingId = listing.Id,
                    BenefitId = benId
                };
                listing.listingBenefits.Add(listingBenefit);
                _context.ListingBenefits.Add(listingBenefit);
            }

            await _context.SaveChangesAsync();

            return listing.Id;
        }

        public async Task FindListingAndChangeStatus(int id)
        {
            var listing = await _context.Listings.FindAsync(id);
            if (listing != null)
            {
                listing.status = listing.status.Equals("Active") ? "Closed" : "Active";
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Listing> FetchListingDetails(int id)
        {
            
            return await _context.Listings
                .Include(l => l.jobType)
                .Include(l => l.town)
                .Include(l => l.Applications)
                .Include(l => l.company)
                .ThenInclude(c => c.industry)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<List<JobType>> GetAllJobTypes()
        {
            return await _context.JobType.ToListAsync();
        }


        public async Task<List<Town>> getAllCities()
        {
            return await _context.Town.ToListAsync();
        }

        public async Task<List<string>> getAllRequirements()
        {
            List<string> requirements = await _context.Requirements
                .Select(r => r.requirement) // Select the requirement name
                .ToListAsync(); // Convert to a List of strings
            return requirements;
        }
        public async Task<List<string>> getAllBenefits()
        {
            List<string> benefits = await _context.Benefits
                .Select(r => r.benefit) // Select the requirement name
                .ToListAsync(); // Convert to a List of strings
            return benefits;
        }

        public void ApplyChanges(EditListingViewModel vm,Listing listing)
        {
            if (listing == null)
                throw new InvalidOperationException("Listing not found.");

            // Update main listing fields
            listing.title = vm.Title;
            listing.datePosted = vm.datePosted;
            listing.Description = vm.Desc;
            listing.experienceLevel = vm.selectedExperienceLevel;
            listing.jobTypeId = vm.selectedJobType;
            listing.townId = vm.selectedTownId;

            // Update Requirements
            var existingReq = _context.ListingRequirements
                .Where(lr => lr.ListingId == listing.Id);
            _context.ListingRequirements.RemoveRange(existingReq);

            var matchedRequirements = _context.Requirements
                .Where(r => vm.selectedRequirements.Contains(r.requirement))
                .ToList();

            var toAddRequirements = matchedRequirements.Select(req => new ListingRequirements
            {
                ListingId = listing.Id,
                RequirementId = req.Id
            });
            _context.ListingRequirements.AddRange(toAddRequirements);

            // Update Benefits
            var existingBenefits = _context.ListingBenefits
                .Where(lb => lb.ListingId == listing.Id);
            _context.ListingBenefits.RemoveRange(existingBenefits);

            var matchedBenefits = _context.Benefits
                .Where(b => vm.selectedBenefits.Contains(b.benefit))
                .ToList();

            var toAddBenefits = matchedBenefits.Select(ben => new ListingBenefits
            {
                ListingId = listing.Id,
                BenefitId = ben.Id
            });
            _context.ListingBenefits.AddRange(toAddBenefits);

            _context.SaveChanges();
        }

    }
}