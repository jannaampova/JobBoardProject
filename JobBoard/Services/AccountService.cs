using JobBoard.Data;
using JobBoard.Dtos;
using JobBoard.Models.plainModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using JobBoard.Security;
using Microsoft.AspNetCore.Identity;

namespace JobBoard.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;  // Changed to ApplicationIdentityDbContext
        private readonly ApplicationIdentityDbContext _iContext;  // Changed to ApplicationIdentityDbContext
        private readonly UserManager<UserData> _userManager;
        private readonly SignInManager<UserData> _signInManager;

        public AccountService(ApplicationIdentityDbContext iContext, ApplicationDbContext context, UserManager<UserData> userManager, SignInManager<UserData> signInManager)
        {
            _iContext = iContext;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Register User using UserManager and IdentityUser
        public async Task<bool> RegisterUserAsync(SignUpRequest model)
        {
            var emailExists = await _userManager.FindByEmailAsync(model.Email);
            if (emailExists != null)
                return false;

            var usernameExists = await _userManager.FindByNameAsync(model.Username);
            if (usernameExists != null)
                return false;

            var newUser = new UserData
            {
                UserName = model.Username,
                Email = model.Email,
                PhoneNumber = model.Phone,
                AccountTypeId = model.AcctTypeId
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
                return false;

            var newAccount = new Account
            {
                UserId = newUser.Id,
                Name = model.Name,
                Username = model.Username,
                Email = model.Email,
                Phone = model.Phone,
                AcctTypeId = model.AcctTypeId,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password)
            };

            try
            {
                _context.Account.Add(newAccount);
                await _context.SaveChangesAsync();

                // 👇 If AccountType = 2, create a Candidate record with AccountId only
                if (model.AcctTypeId == 2)
                {
                    var newCandidate = new Candidate
                    {
                        AccountId = newAccount.Id // assuming this is the foreign key
                    };

                    _context.Candidate.Add(newCandidate);
                    await _context.SaveChangesAsync();
                }
                if (model.AcctTypeId == 1)
                {
                    var newCompany = new Company
                    {
                        accountId = newAccount.Id, // assuming this is the foreign key
                        companyName = model.Name,
                        industryId = 1
                    };

                    _context.Company.Add(newCompany);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("‼️ Грешка при запис в Account или Candidate: " + ex.Message);
                return false;
            }

            return true;
        }



        // Login User using SignInManager and UserManager
        public async Task<LogResult> LogInUserAsync(LogInRequest model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null)
                return LogResult.IncorrectUsername;

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (result.Succeeded)
            {
                return LogResult.Success;
            }
            else
            {
                return LogResult.IncorrectPassword;
            }
        }
    }
}
