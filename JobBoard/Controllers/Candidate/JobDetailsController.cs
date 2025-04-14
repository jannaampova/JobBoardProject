using JobBoard.Models.ViewModels;
using JobBoard.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Controllers.Candidate
{
    public class JobDetailsController : Controller
    {
        private readonly UserManager<UserData> _userManager;

        public JobDetailsController(UserManager<UserData> userManager)
        {
            _userManager = userManager;
        }



        [HttpGet("/details")]
        public async Task<IActionResult> Index()
        {

            UserData currUser = await _userManager.GetUserAsync(User);

            CandidateDashboardViewModel viewModel = new CandidateDashboardViewModel
            {
                user = currUser,

            };

            return View("~/Views/Candidate/Job-Details.cshtml", viewModel);
        }
        public async Task<IActionResult> Apply()
        {

            UserData currUser = await _userManager.GetUserAsync(User);

            CandidateDashboardViewModel viewModel = new CandidateDashboardViewModel
            {
                user = currUser,

            };

            return View("~/Views/Candidate/Apply.cshtml", viewModel);
        }
    }
}
