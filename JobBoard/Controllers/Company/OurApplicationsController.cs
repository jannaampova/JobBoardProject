using JobBoard.Models.ViewModels;
using JobBoard.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Controllers.Company
{
    public class OurApplicationsController : Controller
    {
        private readonly UserManager<UserData> _userManager;
        public OurApplicationsController(UserManager<UserData> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("/ourApplications")]
        public async Task<IActionResult> Index()
        {
            UserData currUser = await _userManager.GetUserAsync(User);

            CompanyDashboardViewModel viewModel = new CompanyDashboardViewModel
            {
                user = currUser,
            };

            return View("~/Views/Company/OurApplications.cshtml", viewModel);
        }

       
        [HttpGet("/Application")]
        public async Task<IActionResult> ViewApplication()
        {
            UserData currUser = await _userManager.GetUserAsync(User);

            CompanyDashboardViewModel viewModel = new CompanyDashboardViewModel
            {
                user = currUser,
            };

            return View("~/Views/Company/ApplicationDetails.cshtml", viewModel);
        }

    }
}
