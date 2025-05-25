using JobBoard.Data;
using JobBoard.Migrations;
using JobBoard.Models.plainModels;
using JobBoard.Models.ViewModels;
using JobBoard.Security;
using JobBoard.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Controllers.Candidate;

public class SavedListingsController : Controller
{
    private readonly UserManager<UserData> _userManager;
    private readonly SavedListingService _savedListingService;
    private readonly ApplicationDbContext _context;

    public SavedListingsController(UserManager<UserData> userManager, SavedListingService savedListingService, ApplicationDbContext context)
    {
        _userManager = userManager;
        _savedListingService = savedListingService;
        _context = context;
    }

    // GET
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
        var candidate = _context.Candidate.Single(c => c.AccountId == account.Id);
        
        List<Listing> listings = _savedListingService.findSavedListingsForCandidate(candidate.Id);

        SavedListingsViewModel model = new SavedListingsViewModel
        {
            user = currUser,
            savedListings = listings,
            candidateId=candidate.Id
        };

        return View("/views/Candidate/Saved-listings.cshtml", model);
    }
    
    [HttpPost]
    public async Task<IActionResult> Unsave(int listingId, int candidateId)
    {
        var service = new SavedListingService(_context);
        service.saveListingToCandidate(listingId, candidateId);

        return RedirectToAction(nameof(Index));
    }
}