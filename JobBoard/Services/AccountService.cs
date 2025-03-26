using JobBoard.Data;
using JobBoard.Models;
using Microsoft.AspNetCore.Identity;

namespace JobBoard.Services
{
    public class AccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<Account> _passwordHasher;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Account>();
        }

        public bool IsEmailTaken(string email)
        {
            return _context.Account.Any(a => a.Email == email);
        }

        public bool ArePasswordsMatching(string password1, string password2)
        {
            return password1 == password2;
        }

        public void RegisterAccount(Account account, string plainPassword)
        {
            account.Password = _passwordHasher.HashPassword(account, plainPassword);

            _context.Account.Add(account);
            _context.SaveChanges();
        }

        public bool VerifyPassword(Account account, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(account, account.Password, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    
}
}
