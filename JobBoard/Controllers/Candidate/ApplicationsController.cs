using JobBoard.Data;
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
        private readonly ApplicationsService _applicationsService;
        private readonly ApplicationDbContext _context;
        public ApplicationsController( UserManager<UserData> userManager, ApplicationsService applicationService, ApplicationDbContext context)
        {
            _userManager = userManager;
            _applicationsService = applicationService;
            _context = context;
        }

        [HttpGet("/applications")]

        public async Task<IActionResult> Index(string? status)
        { 
            UserData currUser = await _userManager.GetUserAsync(User);
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var account = _context.Account.FirstOrDefault(a => a.UserId == userId);
            if (account == null)
            {
                return NotFound("Account not found.");
            }
            var candidate = _context.Candidate.Single(c => c.AccountId == account.Id);
            ApplicationStatus? filter = null;
            if (!string.IsNullOrEmpty(status)
                && Enum.TryParse<ApplicationStatus>(status, true, out var parsed))
            {
                filter = parsed;
            }
            var apps = _applicationsService.FindCandidateApplications(candidate.Id,filter);
            var appsCount = apps.Count;

         


            ApplicationsViewModel viewModel = new ApplicationsViewModel
            {
                user = currUser,
                applications = apps,
                candidateId=candidate.Id,
                count = appsCount,
                CurrentStatus   = filter
            };

            return View("~/Views/Candidate/CandidateApplications.cshtml",viewModel);
        }
    }
}
