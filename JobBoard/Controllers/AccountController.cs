using JobBoard.Dtos;
using JobBoard.Models;
using JobBoard.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using JobBoard.Security;
using JobBoard.Data;

namespace JobBoard.Controllers
{
    [Route("auth")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<UserData> _userManager;
        private readonly SignInManager<UserData> _signInManager;

        public AccountController(IAccountService accountService, UserManager<UserData> userManager,
        SignInManager<UserData> signInManager)
        {
            _accountService = accountService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet("register")]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost("register")]
        public async Task<IActionResult> SignUp(SignUpRequest model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _accountService.RegisterUserAsync(model);
            if (!result)
            {
                ModelState.AddModelError("", "Signup failed. Please try again.");
                return View(model);
            }

            return RedirectToAction("LogIn");
        }

        [HttpGet("login")]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost("logIn")]
        public async Task<IActionResult> LogIn(LogInRequest model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Use SignInManager to check if user exists and login
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.Username);

                // Check the user's role and redirect appropriately
                if (user != null)
                {
                    if (user.AccountTypeId==2)
                    {
                        return RedirectToAction( "Applicant", "CandidateDashboard");
                    }
                    else if (user.AccountTypeId == 1)
                    {
                        return RedirectToAction("Company", "CompanyDashboard");
                    }
                }

                // If no role found, default to dashboard
                return RedirectToAction("Dashboard", "Home");
            }
            else
            {
                // Show errors if login failed
                ModelState.AddModelError("", "Invalid username or password.");
                return View(model);
            }
        }
    }
}
