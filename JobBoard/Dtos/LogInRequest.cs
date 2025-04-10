using System.ComponentModel.DataAnnotations;

namespace JobBoard.Dtos
{
    public class LogInRequest
    {
      

        [Required(ErrorMessage = "Username is required")]

        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]

        public string Password { get; set; }

    }
}
