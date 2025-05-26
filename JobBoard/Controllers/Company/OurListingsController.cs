using JobBoard.Models.plainModels;
using JobBoard.Models.ViewModels;
using JobBoard.Security;
using JobBoard.Services;
using JobBoard.Services.Company;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Controllers.Company
{
    public class OurListingsController : Controller
    {
        private readonly UserManager<UserData> _userManager;
        private readonly CompanyApplicationsService _companyApplicationsService;
        private readonly CreateListingService _listingService;
        private readonly JobDetailsService _jobDetailsService;
        public OurListingsController(UserManager<UserData> userManager, CompanyApplicationsService companyApplicationsService, CreateListingService listingService, JobDetailsService jobDetailsService)
        {
            _userManager = userManager;
            _companyApplicationsService = companyApplicationsService;
            _listingService = listingService;
            _jobDetailsService = jobDetailsService;
        }

        [HttpGet("/ourListings")]
        public async Task<IActionResult> Index()
        {
            UserData currUser = await _userManager.GetUserAsync(User);
            List<Listing> listingsByCompany = _companyApplicationsService.getCompanyListings(currUser);
            CompanyListingViewModel viewModel = new CompanyListingViewModel()
            {
                user = currUser,
                listingsByCompany = listingsByCompany
            };

            return View("~/Views/Company/OurListings.cshtml", viewModel);
        }

        [HttpGet("/editListing")]
        public async Task<IActionResult> EditListing(int id)
        {
            UserData currUser = await _userManager.GetUserAsync(User);
            var listing = await _listingService.FetchListingDetails(id);
            var jobTypes = await _listingService.GetAllJobTypes();
            var cities = await _listingService.getAllCities();
            List<String> allRequirements = await _listingService.getAllRequirements();
            List<String> allBenefits = await _listingService.getAllBenefits();
            List<string> selectedRequirements = await _jobDetailsService.GetRequirements(id); // or similar
            List<string >selectedBenefits = await _jobDetailsService.GetBenefits(id);
            string exLevel=listing.experienceLevel;
            EditListingViewModel viewModel = new EditListingViewModel()
            { 
                user = currUser, 
               selectedListing=listing,
               jobTypes=jobTypes,
               experienceLevels = Enum.GetValues<ExperienceLevel>().ToList(),
               selectedExperienceLevel = exLevel,
               towns = cities,
               selectedTownId = listing.town.Id,
               selectedJobType = listing.jobType.Id,
               requirements=allRequirements,
               benefits=allBenefits,
               selectedRequirements = selectedRequirements,
               selectedBenefits = selectedBenefits
                   
            };

            return View("~/Views/Company/EditListing.cshtml", viewModel);
        }
      
        [HttpGet("/listingDetails")]
        public async Task<IActionResult> ViewListing(int id)
        {
            UserData currUser = await _userManager.GetUserAsync(User);
            Listing listing = await _listingService.FetchListingDetails(id);
            List<string> requirementsList = await _jobDetailsService.GetRequirements(id);
            List<string> benefitsList = await _jobDetailsService.GetBenefits(id);
            CompanyListingViewModel viewModel = new CompanyListingViewModel()
            {
                user = currUser,
                selectedListing = listing,
                requirements = requirementsList,
                benefits = benefitsList
            };

            return View("~/Views/Company/ListingDetails.cshtml", viewModel);
        }

        public async Task<IActionResult> ChangeStatus(int id)
        {
           await _listingService.FindListingAndChangeStatus(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SubmitChanges(EditListingViewModel viewModel)
        {
            var selectedListing = await _listingService.FetchListingDetails(viewModel.selectedListing.Id);
            _listingService.ApplyChanges(viewModel, selectedListing);
            return RedirectToAction("Index");
        }
    }
}
