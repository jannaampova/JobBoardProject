using JobBoard.Data;
using JobBoard.Models.plainModels;
using JobBoard.Models.ViewModels;
using JobBoard.Security;
using JobBoard.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace JobBoard.Controllers.Candidate
{
    public class JobDetailsController : Controller
    {
        private readonly UserManager<UserData> _userManager;
        private ApplicationDbContext _context;
        private JobDetailsService _jobDetailsService;

        public JobDetailsController(UserManager<UserData> userManager, ApplicationDbContext context, JobDetailsService jobDetailsService)
        {
            _userManager = userManager;
            _context = context;
            _jobDetailsService = jobDetailsService; 
        }



        [HttpGet("/details")]
        public async Task<IActionResult> Index(int id)
        {
            UserData currUser = await _userManager.GetUserAsync(User);

            Listing job= await _jobDetailsService.getlistedJob(id).ConfigureAwait(false);
            List<string> requirementsList = _jobDetailsService.GetRequirements(job.Id);
            List<string> benefitsList = _jobDetailsService.GetBenefits(job.Id);

            if (job == null)
            {
                return NotFound();
            }

            JobDetailsModel viewModel = new JobDetailsModel
            {
                user = currUser,
                jobListed = job,
                requirements = requirementsList,
                benefits = benefitsList
            };

            return View("~/Views/Candidate/Job-Details.cshtml", viewModel);
        }

        //TOVA DOLNOTO NE TRQBVA DA E TAKA A S NESHTA ZA APPLY
        public async Task<IActionResult> Apply(int id)
        {

            UserData currUser = await _userManager.GetUserAsync(User);
            Listing job = await _jobDetailsService.getlistedJob(id).ConfigureAwait(false);
            List<string> requirementsList = _jobDetailsService.GetRequirements(job.Id);
            List<string> benefitsList = _jobDetailsService.GetBenefits(job.Id);

            JobDetailsModel viewModel = new JobDetailsModel
            {
                user = currUser,
                jobListed = job,
                requirements = requirementsList,
                benefits = benefitsList
            };

            return View("~/Views/Candidate/Apply.cshtml", viewModel);
        }

    

    }
}
