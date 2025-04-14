using JobBoard.Migrations;
using JobBoard.Models.plainModels;
using JobBoard.Models.ViewModels;
using JobBoard.Security;
using JobBoard.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Controllers.Candidate
{
    public  class ApplicationsController : Controller
    {
        private readonly UserManager<UserData> _userManager;

        public ApplicationsController( UserManager<UserData> userManager)
        {
            _userManager = userManager;
        }



        [HttpGet("/applications")]

        public async Task<IActionResult> Index()
        { 
            UserData currUser = await _userManager.GetUserAsync(User);

            CandidateDashboardViewModel viewModel = new CandidateDashboardViewModel
            {
                user = currUser,
            };

            return View("~/Views/Candidate/CandidateApplications.cshtml",viewModel);
        }
    }
}
