using Microsoft.AspNetCore.Identity;
using JobBoard.Models.plainModels;
using JobBoard.Models.ViewModels;
using JobBoard.Security;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Controllers.Company
{
    public class CompanyDashboardController : Controller
    {
        private readonly UserManager<UserData> _userManager;

        public CompanyDashboardController(UserManager<UserData> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Company()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("LogIn", "Account");
            }

            var viewModel = new CompanyDashboardViewModel
            {
                user = user
            };

            return View("~/Views/Dashboards/CompanyDashboard.cshtml", viewModel);
        }
    }
}
