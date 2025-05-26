using JobBoard.Data;
using JobBoard.Models.plainModels;
using JobBoard.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Services.Company
{
    public class CompanyDashboardService
    {
        private readonly ApplicationDbContext _context;

        public CompanyDashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CompanyDashboardViewModel> GetCompanyDashboardData(string userId)
        {
            var account = await _context.Account
                .FirstOrDefaultAsync(a => a.UserId == userId);

            if (account == null)
                throw new ArgumentException("Account not found");

            var company = await _context.Company
                .Include(c => c.industry)
                .FirstOrDefaultAsync(c => c.accountId == account.Id);

            var listings = await _context.Listings
                .Include(l => l.company)
                .Where(l => l.companyId == company.Id)
                .ToListAsync();

            var applications = await _context.Application
                .Include(a => a.Listing)
                .Include(a => a.Candidate)
                .Where(a => a.Listing.companyId == company.Id)
                .ToListAsync();

            int pendingApplicationsCount = applications
                .Count(a => a.Status == ApplicationStatus.PENDING);

            int activeListingsCount = listings
                .Count(l => l.status == "Active");

            int totalApplicationsCount = applications.Count;

            return new CompanyDashboardViewModel()
            {
                PendingReviews    = pendingApplicationsCount,
                ActiveListings          = activeListingsCount,
                TotalApplications       = totalApplicationsCount
            };
        }
    }
}

