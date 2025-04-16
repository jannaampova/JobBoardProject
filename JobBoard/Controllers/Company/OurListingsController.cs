using JobBoard.Models.ViewModels;
using JobBoard.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Controllers.Company
{
    public class OurListingsController : Controller
    {
        private readonly UserManager<UserData> _userManager;
        public OurListingsController(UserManager<UserData> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("/ourListings")]
        public async Task<IActionResult> Index()
        {
            UserData currUser = await _userManager.GetUserAsync(User);

            CompanyDashboardViewModel viewModel = new CompanyDashboardViewModel
            {
                user = currUser,
            };

            return View("~/Views/Company/OurListings.cshtml", viewModel);
        }

        [HttpGet("/editListing")]
        public async Task<IActionResult> EditListing()
        {
            UserData currUser = await _userManager.GetUserAsync(User);

            CompanyDashboardViewModel viewModel = new CompanyDashboardViewModel
            {
                user = currUser,
            };

            return View("~/Views/Company/EditListing.cshtml", viewModel);
        }
      
        [HttpGet("/listingDetails")]
        public async Task<IActionResult> ViewListing()
        {
            UserData currUser = await _userManager.GetUserAsync(User);

            CompanyDashboardViewModel viewModel = new CompanyDashboardViewModel
            {
                user = currUser,
            };

            return View("~/Views/Company/ListingDetails.cshtml", viewModel);
        }
     
    }
}
