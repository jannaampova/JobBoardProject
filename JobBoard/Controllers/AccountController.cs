using JobBoard.Dtos;
using JobBoard.Models;
using JobBoard.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Controllers
{
    [Route("auth")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
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

            return RedirectToAction("login");
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
            LogResult res = await _accountService.LogInUserAsync(model);
            if (res !=LogResult.Success ) {
                switch (res)
                {
                    case LogResult.IncorrectUsername: ModelState.AddModelError(nameof(model.Username), "Username not found.");break;
                    case LogResult.IncorrectPassword: ModelState.AddModelError(nameof(model.Password), "Incorrect password."); break;
                }
                return View(model);
            }
            return RedirectToAction("register");

        }

    }
}
