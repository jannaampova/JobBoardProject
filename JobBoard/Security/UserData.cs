using JobBoard.Models;
using Microsoft.AspNetCore.Identity;

namespace JobBoard.Security
{
    public class UserData: IdentityUser
    {
        public int AccountTypeId { get; set; }
    }
}
