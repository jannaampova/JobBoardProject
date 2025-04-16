using JobBoard.Models.ViewModels;
using JobBoard.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Controllers.Company
{
    public class CreateListingController : Controller
    {
        private readonly UserManager<UserData> _userManager;
        public CreateListingController(UserManager<UserData> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("/createListing")]

        public async Task<IActionResult> Index()
        {
            UserData currUser = await _userManager.GetUserAsync(User);

            CompanyDashboardViewModel viewModel = new CompanyDashboardViewModel
            {
                user = currUser,
            };

            return View("~/Views/Company/CreateListing.cshtml", viewModel);
        }
    }
}
