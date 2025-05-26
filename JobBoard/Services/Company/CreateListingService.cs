﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobBoard.Data;
using JobBoard.Models.plainModels;
using JobBoard.Models.ViewModels;

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
    }
}