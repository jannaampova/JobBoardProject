using JobBoard.Data;
using JobBoard.Models;
using JobBoard.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Controllers
{

    public class AccountController : Controller
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        public IActionResult SignUp(Account account, string password2)
        {
            if (!ModelState.IsValid)
            {
                return View(account);
            }

            if (!_accountService.ArePasswordsMatching(account.Password, password2))
            {
                ModelState.AddModelError("Password", "Passwords do not match.");
                return View(account);
            }

            if (_accountService.IsEmailTaken(account.Email))
            {
                ModelState.AddModelError("Email", "Email is already in use.");
                return View(account);
            }

            _accountService.RegisterAccount(account, password2); // Pass the plain password for hashing
            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            return View();
        }
   
    }
}
