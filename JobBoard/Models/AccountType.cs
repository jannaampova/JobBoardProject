using System.ComponentModel.DataAnnotations;

namespace JobBoard.Models
{
    public class AccountType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string accType { get; set; }
        public ICollection<Account> Accounts { get; set; } = new List<Account>();

    }
}
