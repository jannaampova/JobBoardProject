using JobBoard.Models.ViewModels;
using JobBoard.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Controllers.Candidate
{
    public class AccountSettingsController : Controller
    {
        private readonly UserManager<UserData> _userManager;
        public AccountSettingsController(UserManager<UserData> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet("/settings")]
        public async Task<IActionResult> Index()
        {
            UserData currUser = await _userManager.GetUserAsync(User);

            CandidateDashboardViewModel viewModel = new CandidateDashboardViewModel
            {
                user = currUser,
            };

            return View("~/Views/Candidate/AccountSettings.cshtml", viewModel);
        }
    }
}
