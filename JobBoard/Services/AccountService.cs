using JobBoard.Data;
using JobBoard.Dtos;
using JobBoard.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using BCrypt.Net;
namespace JobBoard.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterUserAsync(SignUpRequest model)
        {
            if (await _context.Account.AnyAsync(a => a.Email == model.Email))
                return false;  // Email already exists

            var newUser = new Account
            {
                Name = model.Name,
                Username = model.Username,
                Email = model.Email,
                Phone = model.Phone,
                AcctTypeId = model.AcctTypeId,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password)
            };

            _context.Account.Add(newUser);
            await _context.SaveChangesAsync();
            return true;
        }
    } 
}
