using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JobBoard.Models.plainModels
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }  // 🔥 Link to Identity user

        [Required(ErrorMessage = "Full Name/Company Name is required")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }


        [Required]
        public int AcctTypeId { get; set; }

        [ForeignKey("AcctTypeId")]
        public AccountType AccountType { get; set; }
        public List<SavedListings> SavedListings { get; set; }

    }
}
