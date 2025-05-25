using JobBoard.Data;
using JobBoard.Models.ViewModels;
using JobBoard.Security;
using JobBoard.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JobBoard.Controllers.Candidate
{
    public class AccountSettingsController : Controller
    {
        private readonly UserManager<UserData> _userManager;
        private readonly AccountSettingsService _accountSettingsService;
        private readonly ApplicationDbContext _context;
        public AccountSettingsController(
            UserManager<UserData> userManager,
            AccountSettingsService accountSettingsService,
            ApplicationDbContext context)
        {
            _userManager               = userManager;
            _accountSettingsService    = accountSettingsService;
            _context                   = context;
        }
        
        //GET
        [HttpGet("/settings")]
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
            
            var allSkills = _context.Skills.ToList();
            var selectedSkillIds = _context.CandidateSkills
                .Where(cs => cs.CandidateId == candidate.Id)
                .Select(cs => cs.SkillId)
                .ToList();
            
            AccountSettingsViewModel viewModel = new AccountSettingsViewModel()
            {
                CandidateId = candidate.Id,
                AccountId=candidate.AccountId,
                user = currUser,
                FullName = candidate.Account.Name,
                Email=candidate.Account.Email,
                Phone=candidate.Account.Phone,
                PhotoPath = candidate.PhotoPath,
                ResumePath = candidate.ResumePath,
                AllSkills=allSkills,
                SelectedSkillIds=selectedSkillIds
            };

            return View("~/Views/Candidate/AccountSettings.cshtml", viewModel);
        }
        
        // POST DETAILS
        [HttpPost("/settings/details")]
        public async Task<IActionResult> ChangeDetails(int candidateId, string FullName, string Email, string Phone)
        {
            _accountSettingsService.ChangeDetails(candidateId, FullName, Email, Phone);;
            return RedirectToAction(nameof(Index));
        }
        
        // POST PHOTO
        [HttpPost("/settings/photo")]
        public async Task<IActionResult> UploadPhoto(int candidateId, string imageUrl)
        {
            _accountSettingsService.UpdatePhotoPath(candidateId, imageUrl);
            return RedirectToAction(nameof(Index));
        }
        
        // POST RESUME
        [HttpPost("/settings/resume")]
        public async Task<IActionResult> UploadResume(int candidateId, string resumeUrl)
        {
            _accountSettingsService.UpdateResumePath(candidateId, resumeUrl);
            return RedirectToAction(nameof(Index));
        }
        
        // POST SKILLS
        [HttpPost("/settings/skills")]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateSkills(int candidateId, List<int> selectedSkillIds)
        {
            selectedSkillIds ??= new List<int>();

            _accountSettingsService.UpdateSkills(candidateId, selectedSkillIds);
            return RedirectToAction(nameof(Index));
        }
    }
    
}
