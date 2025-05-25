using JobBoard.Data;
using JobBoard.Models.plainModels;
using JobBoard.Models.ViewModels;
using JobBoard.Security;
using JobBoard.Services.Company;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Controllers.Company
{
    public class OurApplicationsController : Controller
    {
        private readonly UserManager<UserData> _userManager;
        private readonly CompanyApplicationsService _companyApplicationsService;
        
        public OurApplicationsController(UserManager<UserData> userManager, CompanyApplicationsService companyApplicationsService)
        {
            _userManager = userManager;
            _companyApplicationsService = companyApplicationsService;
            
        }

        [HttpGet("/ourApplications")]
        public async Task<IActionResult> Index()
        {
            UserData currUser = await _userManager.GetUserAsync(User);

            List<Application> companyListings = _companyApplicationsService.FindCompanyApplications(currUser);
 
            CompanyApplications viewModel = new CompanyApplications()
            {
                user = currUser,
                Applications = companyListings
            };

            return View("~/Views/Company/OurApplications.cshtml", viewModel);
        }

       
        [HttpGet("/Application")]
        public async Task<IActionResult> ViewApplication(int id)
        {
            UserData currUser = await _userManager.GetUserAsync(User);
            Application application=await _companyApplicationsService.fetchApplicationInfo(id);

            CompanyApplications viewModel = new CompanyApplications()
            {
                user = currUser,
                application=application
            };

            return View("~/Views/Company/ApplicationDetails.cshtml", viewModel);
        }

        public IActionResult StatusChangeAccept(int id)
        {
            _companyApplicationsService.changeStatus(id,ApplicationStatus.ACCEPTED);
            return RedirectToAction("Index"); 

        }
        public IActionResult StatusChangeDecline(int id)
        {
            _companyApplicationsService.changeStatus(id,ApplicationStatus.REJECTED);
            return RedirectToAction("Index"); 
            
        }
    }
}
