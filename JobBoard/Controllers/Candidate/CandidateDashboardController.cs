using JobBoard.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobBoard.Services;
using JobBoard.Migrations;
using JobBoard.Models.ViewModels;
using JobBoard.Security;
using Microsoft.AspNetCore.Identity;
using JobBoard.Models.plainModels;

namespace JobBoard.Controllers.Candidate
{

    public class CandidateDashboardController : Controller
    {

        private readonly FilterService filterService;
        private readonly UserManager<UserData> _userManager;
        private readonly ApplicationDbContext _context;

        public CandidateDashboardController(FilterService filters, UserManager<UserData> userManager, ApplicationDbContext context)
        {
            filterService = filters;
            _userManager = userManager;
            _context = context;
        }
        [HttpGet("applicantDashboard")]

        public async Task<IActionResult> Applicant()
        {
            List<Industry> industries = filterService.getIndustries();
            List<JobType> jobTypes = filterService.getJobTypes();
            List<Town> cities = filterService.getCities();
            List<string> expLevels = filterService.getExperienceLevel();
            List<Listing> listings = filterService.GetAllJobListings();


            UserData currUser = await _userManager.GetUserAsync(User);

            CandidateDashboardViewModel viewModel = new CandidateDashboardViewModel
            {
                Industries = industries,
                JobTypes = jobTypes,
                user = currUser,
                Cities = cities,
                Levels = expLevels,
                AllListings = listings

            };

            return View("~/Views/Dashboards/CandidateDashboard.cshtml", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Filter(CandidateDashboardViewModel filters)
        {
            UserData currUser = await _userManager.GetUserAsync(User);

            CandidateDashboardViewModel filteredResults = filterService.GetFilteredJobs(filters);
            filteredResults.SelectedIndustries = filters.SelectedIndustries;
            filteredResults.SelectedJobTypes = filters.SelectedJobTypes;
            filteredResults.SelectedCities = filters.SelectedCities;
            filteredResults.SelectedLevels = filters.SelectedLevels;
            filteredResults.SearchQuery = filters.SearchQuery;
            filteredResults.user = currUser;


            return View("~/Views/Dashboards/CandidateDashboard.cshtml", filteredResults);
        }

        [HttpPost]
        public IActionResult SaveListing(int listingId)
        {
            var service = new SavedListingService(_context);

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var account = _context.Account.FirstOrDefault(a => a.UserId == userId);
            if (account == null)
            {
                return NotFound("Account not found.");
            }
            var candidate = _context.Candidate.Single(c => c.AccountId == account.Id);

            service.saveListingToCandidate(listingId, candidate.Id);

            return RedirectToAction("Applicant", "CandidateDashboard");
        }
        public IActionResult SaveListingFromJobDetails(int listingId)
        {
            var service = new SavedListingService(_context);

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var account = _context.Account.FirstOrDefault(a => a.UserId == userId);
            if (account == null)
            {
                return NotFound("Account not found.");
            }
            var candidate = _context.Candidate.Single(c => c.AccountId == account.Id);

            service.saveListingToCandidate(listingId, candidate.Id);

            return RedirectToAction("Index", "JobDetails", new { id = listingId });
        }





    }
}
