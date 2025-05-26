using JobBoard.Data;
using JobBoard.Models.ViewModels;
using JobBoard.Security;
using JobBoard.Services;
using JobBoard.Services.Company;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Controllers.Company
{
    public class OurCompanyController(
        UserManager<UserData> userManager,
        CompanySettingsService companySettingsService,
        ApplicationDbContext context)
        : Controller
    {
        private readonly CompanySettingsService _companySettingsService = companySettingsService;

        [HttpGet("/ourCompany")]
        public async Task<IActionResult> Index()
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
            var company = context.Company.Single(c => c.accountId == account.Id);
            var allIndustries = context.Industry.ToList();


            CompanySettingsViewModel viewModel = new CompanySettingsViewModel
            {
                user = currUser,
                AccountId = account.Id,
                CompanyId = company.Id,
                AllIndustries = allIndustries,
                Phone = account.Phone,
                Email=account.Email,
                Name=account.Name,
                PhotoPath=company.photoPath,
                WebURL=company.webUrl,
                Industry=company.industry.industry,
                Description = company.description
            };

            return View("~/Views/Company/OurCompany.cshtml", viewModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> ChangeDetails( int CompanyId,int AccountId,
            string Name,
            string Industry,
            string Description,
            string PhotoUrl)
        {
            _companySettingsService.ChangeDetails(CompanyId, AccountId, Name, Industry, Description, PhotoUrl);
            return RedirectToAction("Index");
            
        }
        
        [HttpPost]
        public async Task<IActionResult> ChangeAccount( int CompanyId,int AccountId, string Email, string Phone, string WebURL)
        {
            _companySettingsService.ChangeAccount(CompanyId, AccountId, Email, Phone, WebURL);
            return RedirectToAction("Index");

        }
    }
}
