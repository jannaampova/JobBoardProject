using JobBoard.Data;
using JobBoard.Models.ViewModels;
using JobBoard.Security;
using JobBoard.Services.Company;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Controllers.Company
{
    public class PeopleController(
        UserManager<UserData> userManager,
        ApplicationDbContext applicationDbContext,
        PeopleService peopleService)
        : Controller
    {
        private readonly PeopleService _peopleService = peopleService;

        [HttpGet("/people")]
        public async Task<IActionResult> Index()
        {
            UserData currUser = await userManager.GetUserAsync(User);
            var allCandidates = applicationDbContext.Candidate
                .Include(c => c.Account)
                .ToList();            PeopleViewModel viewModel = new PeopleViewModel()
            {
                user = currUser,
                Candidates = allCandidates
            };

            return View("~/Views/Company/People.cshtml", viewModel);
        }
      

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ViewProfile(int id)
        {
            UserData currUser = await userManager.GetUserAsync(User);
            var candidate = await applicationDbContext.Candidate
                .Include(c => c.Account)  // eager-load the Account nav prop
                .FirstOrDefaultAsync(c => c.Id == id);

            PeopleViewModel viewModel = new PeopleViewModel()
            {
                user = currUser,
                FullName=candidate.Account.Name,
                Email=candidate.Account.Email,
                Phone=candidate.Account.Phone,
                PhotoPath = candidate.PhotoPath,
                ResumePath = candidate.ResumePath,
                ExperienceLevel = candidate.ExperienceLevel,
                Education=candidate.Education
                
            };

            return View("~/Views/Company/CandidateProfile.cshtml", viewModel);
        }
     
    }
}
