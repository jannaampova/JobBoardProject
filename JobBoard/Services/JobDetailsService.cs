using JobBoard.Data;
using JobBoard.Models;
using JobBoard.Models.plainModels;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Services
{
    public class JobDetailsService
    {
        private readonly ApplicationDbContext _context;
        public JobDetailsService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Listing> getlistedJob(int id)
        {
            var job = await _context.Listings
                                     .Include(j => j.company)
                                     .Include(j => j.town)
                                     .Include(j => j.jobType)
                                     .Include(j => j.company.industry)
                                     .FirstOrDefaultAsync(j => j.Id == id);
            return job;
        }

        public List<string> GetRequirements(int id)
        {
            List<string> requirements = _context.ListingRequirements
                .Where(r => r.ListingId == id) // Filter by listingId
                .Select(r => r.Requirement.requirement) // Select the requirement name
                .ToList(); // Convert to a List of strings
            return requirements;
        }
        public List<string> GetBenefits(int listingId)
        {
            List<string> benefits = _context.ListingBenefits
                .Where(b => b.ListingId == listingId) 
                .Select(b => b.Benefit.benefit)
                .ToList();
            return benefits;
        }
    }
}
