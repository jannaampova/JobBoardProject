using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobBoard.Data;
using JobBoard.Models.plainModels;

namespace JobBoard.Services.Company
{
    public class CreateListingService
    {
        private readonly ApplicationDbContext _context;
        public CreateListingService(ApplicationDbContext context)
        {
            _context = context;
        }

 
        public async Task<int> CreateListingAsync(
            int companyId,
            string title,
            string description,
            DateOnly datePosted,
            int jobTypeId,
            int townId,
            string experienceLevel,
            IEnumerable<int> requirementIds,
            IEnumerable<int> benefitIds)
        {
            var listing = new Listing
            {
                companyId        = companyId,
                title            = title,
                Description      = description,
                datePosted       = datePosted,
                jobTypeId        = jobTypeId,
                townId           = townId,
                experienceLevel  = experienceLevel,
                status="Active",
                listingRequirements = new List<ListingRequirements>(),
                listingBenefits     = new List<ListingBenefits>()
            };

            _context.Listings.Add(listing);
            await _context.SaveChangesAsync();

            foreach (var reqId in requirementIds)
            {
                listing.listingRequirements.Add(new ListingRequirements
                {
                    ListingId     = listing.Id,
                    RequirementId = reqId
                });
            }

            foreach (var benId in benefitIds)
            {
                listing.listingBenefits.Add(new ListingBenefits
                {
                    ListingId  = listing.Id,
                    BenefitId  = benId
                });
            }

            await _context.SaveChangesAsync();

            return listing.Id;
        }
    }
}
