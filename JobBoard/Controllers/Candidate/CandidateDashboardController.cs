using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobBoard.Data; // or your actual namespace
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

        public CandidateDashboardController(FilterService filters, UserManager<UserData> userManager)
        {
            filterService = filters;
            _userManager = userManager;
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
        public IActionResult Filter(CandidateDashboardViewModel filters)
        {

            CandidateDashboardViewModel filteredResults = filterService.GetFilteredJobs(filters);
            filteredResults.SelectedIndustries = filters.SelectedIndustries;
            filteredResults.SelectedJobTypes = filters.SelectedJobTypes;
            filteredResults.SelectedCities = filters.SelectedCities;
            filteredResults.SelectedLevels = filters.SelectedLevels;
            filteredResults.SearchQuery = filters.SearchQuery;


            return View("~/Views/Dashboards/CandidateDashboard.cshtml", filteredResults);
        }





    }
}
