using JobBoard.Models.ViewModels;
using JobBoard.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Controllers.Candidate
{
    public class SavedListingsController : Controller
    {
        private readonly UserManager<UserData> _userManager;

        public SavedListingsController(UserManager<UserData> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("/listings")]
        public async Task<IActionResult> Index()
        {
            UserData currUser = await _userManager.GetUserAsync(User);

            CandidateDashboardViewModel viewModel = new CandidateDashboardViewModel
            {
                user = currUser,
            };
            return View("~/Views/Candidate/Saved-listings.cshtml",viewModel);

        }
    }
}
