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
    public class JobDetailsController(
        UserManager<UserData> userManager,
        ApplicationDbContext context,
        JobDetailsService jobDetailsService,
        ApplyService applyService)
        : Controller
    {
        [HttpGet("/details")]
        public async Task<IActionResult> Index(int id)
        {
            UserData currUser = await userManager.GetUserAsync(User);

            Listing job= await jobDetailsService.getlistedJob(id).ConfigureAwait(false);
            List<string> requirementsList = jobDetailsService.GetRequirements(job.Id);
            List<string> benefitsList = jobDetailsService.GetBenefits(job.Id);

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
        
        public async Task<IActionResult> Apply(int id)
        {

            UserData currUser = await userManager.GetUserAsync(User);

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var account = context.Account.FirstOrDefault(a => a.UserId == userId);
            if (account == null)
            {
                return NotFound("Account not found.");
            }
            var candidate = context.Candidate.Single(c => c.AccountId == account.Id);
            
            Listing job= await jobDetailsService.getlistedJob(id).ConfigureAwait(false);
            bool hasApplied = await context.Application
                .AnyAsync(a => a.CandidateId == candidate.Id 
                               && a.ListingId   == job.Id);


            ApplyViewModel viewModel = new ApplyViewModel()
            {
                user = currUser,
                PhotoPath = candidate.PhotoPath,
                Email=candidate.Account.Email,
                FullName = candidate.Account.Name,
                Phone=candidate.Account.Phone,
                ResumePath = candidate.ResumePath,
                jobListed = job,
                CandidateId = candidate.Id,
                JobId=job.Id,
                HasApplied   = hasApplied  
            };

            return View("~/Views/Candidate/Apply.cshtml",viewModel);
        }
        
        public async Task<IActionResult> SubmitApplication(int candidateId, int jobId)
        {
            var candidate = context.Candidate.Single(c => c.Id == candidateId);
            var job = context.Listings.Single(l=>l.Id==jobId);
            var result = await applyService.SubmitApplication(candidate,job);
            if (!result)
            {
                ModelState.AddModelError("", "Signup failed. Please try again.");
            }

            return RedirectToAction(nameof(Index), new { id = jobId });
        }
    }
}
