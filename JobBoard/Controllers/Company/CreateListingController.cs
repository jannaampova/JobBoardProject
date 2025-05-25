using JobBoard.Data;
using JobBoard.Dtos;
using JobBoard.Models.plainModels;
using JobBoard.Models.ViewModels;
using JobBoard.Security;
using JobBoard.Services;
using JobBoard.Services.Company;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Controllers.Company
{
    public class CreateListingController : Controller
    {
        private readonly UserManager<UserData> _userManager;
        private readonly CreateListingService _createListingService;
        private readonly ApplicationDbContext _context;
        public CreateListingController(
            UserManager<UserData> userManager,
            CreateListingService createListingService,
            ApplicationDbContext context)
        {
            _userManager               = userManager;
            _createListingService    = createListingService;
            _context                   = context;
        }
        
  
        [HttpGet("/createListing")]

        public async Task<IActionResult> Index()
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
            var company = _context.Company.Single(c => c.accountId == account.Id);
            company = _context.Company
                .Include(c => c.industry) 
                .Single(c => c.accountId == account.Id);


            CreateListingViewModel viewModel = new CreateListingViewModel()
            {
                user = currUser,
                CompanyId = company.Id,
                Industry=company.industry.industry,
                cities = _context.Town.ToList(),
                jobTypes = _context.JobType.ToList(),
                levels = Enum.GetValues<ExperienceLevel>().ToList(),
                Benefits = _context.Benefits.ToList(),
                Requirements = _context.Requirements.ToList()
            };

            return View("~/Views/Company/CreateListing.cshtml", viewModel);
        }

        [HttpPost("/createListing")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Publish(CreateListingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
      
            var newListingId = await _createListingService.CreateListingAsync(model);

            return RedirectToAction("Index", "CreateListing");
        }
    }
}
