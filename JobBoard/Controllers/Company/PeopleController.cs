using JobBoard.Models.ViewModels;
using JobBoard.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace JobBoard.Controllers.Company
{
    public class PeopleController : Controller
    {
        private readonly UserManager<UserData> _userManager;
        public PeopleController(UserManager<UserData> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("/people")]
        public async Task<IActionResult> Index()
        {
            UserData currUser = await _userManager.GetUserAsync(User);

            CompanyDashboardViewModel viewModel = new CompanyDashboardViewModel
            {
                user = currUser,
            };

            return View("~/Views/Company/People.cshtml", viewModel);
        }
      

        [HttpGet("/candidateProfile")]
        public async Task<IActionResult> ViewProfile()
        {
            UserData currUser = await _userManager.GetUserAsync(User);

            CompanyDashboardViewModel viewModel = new CompanyDashboardViewModel
            {
                user = currUser,
            };

            return View("~/Views/Company/CandidateProfile.cshtml", viewModel);
        }
     
    }
}
