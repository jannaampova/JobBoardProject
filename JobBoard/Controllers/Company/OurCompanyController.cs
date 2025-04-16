using JobBoard.Models.ViewModels;
using JobBoard.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Controllers.Company
{
    public class OurCompanyController : Controller
    {
        private readonly UserManager<UserData> _userManager;
        public OurCompanyController(UserManager<UserData> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("/ourCompany")]
        public async Task<IActionResult> Index()
        {
            UserData currUser = await _userManager.GetUserAsync(User);

            CompanyDashboardViewModel viewModel = new CompanyDashboardViewModel
            {
                user = currUser,
            };

            return View("~/Views/Company/OurCompany.cshtml", viewModel);
        }
      
    }
}
