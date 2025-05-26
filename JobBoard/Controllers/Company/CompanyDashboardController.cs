using JobBoard.Data;
using JobBoard.Models.plainModels;
using JobBoard.Models.ViewModels;
using JobBoard.Security;
using JobBoard.Services.Company;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Controllers.Company
{
    public class CompanyDashboardController : Controller
    {
        private readonly UserManager<UserData> _userManager;
        private readonly CompanyDashboardService _dashboardService;

        public CompanyDashboardController(
            UserManager<UserData> userManager,
            CompanyDashboardService dashboardService)
        {
            _userManager = userManager;
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Company()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("LogIn", "Account");

            var viewModel = await _dashboardService.GetCompanyDashboardData(user.Id);

            viewModel.user = user;

            return View("~/Views/Dashboards/CompanyDashboard.cshtml", viewModel);
        }
    }
}