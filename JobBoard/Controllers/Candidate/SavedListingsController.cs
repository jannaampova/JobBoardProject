using JobBoard.Data;
using JobBoard.Security;
using JobBoard.Services;
using JobBoard.Dtos;
using JobBoard.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace JobBoard.Views.Candidate
{
    [Authorize]


    public class SavedListingsController : Controller
    {
        private readonly UserManager<UserData> _userManager;
        private ApplicationDbContext _context;
        private SavedListingService _savedListingService;

        public SavedListingsController(UserManager<UserData> userManager, ApplicationDbContext context,SavedListingService savedListingService)
        {
            _userManager = userManager;
            _context = context;
            _savedListingService = savedListingService;
        }
        [HttpPost]
    
        public async Task<IActionResult> SaveListing(int id)
        {
            UserData currUser = await _userManager.GetUserAsync(User);
            

            _savedListingService.SaveListing(currUser, id);

            return RedirectToAction("Applicant", "CandidateDashboard");

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
