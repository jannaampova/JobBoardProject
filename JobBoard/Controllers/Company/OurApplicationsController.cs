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
        public async Task<IActionResult> Index(string? status)
        {
            UserData currUser = await _userManager.GetUserAsync(User);
            ApplicationStatus? filter = null;
            if (!string.IsNullOrEmpty(status)
                && Enum.TryParse<ApplicationStatus>(status, true, out var parsed))
            {
                filter = parsed;
            }
            List<Application> companyListings = _companyApplicationsService.FindCompanyApplications(currUser,filter);
 
            CompanyApplicationsViewModel viewModel = new CompanyApplicationsViewModel()
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
            var candidateId = application.CandidateId;
            List<CandidateSkills> skills = _companyApplicationsService.FindCandidateSkills(candidateId);

            CompanyApplicationsViewModel viewModel = new CompanyApplicationsViewModel()
            {
                user = currUser,
                application = application,
                candidateSkills = skills
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
