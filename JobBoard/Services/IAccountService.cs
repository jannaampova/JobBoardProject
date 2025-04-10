using JobBoard.Dtos;

namespace JobBoard.Services
{
    public interface IAccountService
    {
 
            Task<bool> RegisterUserAsync(SignUpRequest model);
            Task<LogResult> LogInUserAsync(LogInRequest model);

    }
}
